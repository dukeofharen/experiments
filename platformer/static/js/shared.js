function initialize(game){
    //  When a State is added to Phaser it automatically has the following properties set on it, even if they already exist:

    game.game;      //  a reference to the currently running game (Phaser.Game)
    game.add;       //  used to add sprites, text, groups, etc (Phaser.GameObjectFactory)
    game.camera;    //  a reference to the game camera (Phaser.Camera)
    game.cache;     //  the game cache (Phaser.Cache)
    game.input;     //  the global input manager. You can access this.input.keyboard, this.input.mouse, as well from it. (Phaser.Input)
    game.load;      //  for preloading assets (Phaser.Loader)
    game.math;      //  lots of useful common math operations (Phaser.Math)
    game.sound;     //  the sound manager - add a sound, play one, set-up markers, etc (Phaser.SoundManager)
    game.stage;     //  the game stage (Phaser.Stage)
    game.time;      //  the clock (Phaser.Time)
    game.tweens;    //  the tween manager (Phaser.TweenManager)
    game.state;     //  the state manager (Phaser.StateManager)
    game.world;     //  the game world (Phaser.World)
    game.particles; //  the particle manager (Phaser.Particles)
    game.physics;   //  the physics manager (Phaser.Physics)
    game.rnd;       //  the repeatable random number generator (Phaser.RandomDataGenerator)
    game.levelStateName;
    game.levelName;
    game.nextLevel;
    game.player;
    game.healthText;
    game.scoreText;
    game.levelText;
    game.map;
    game.layer;
    game.jumpButton;
    game.cursors;
    game.jumpTimer = 0;
    game.facing = 'left';
    game.stars;
    game.baddies;
    game.baddieMarkers;
    game.stillHealing;
    game.music;

    var savedGame = storage.get();
    game.health = savedGame.health || 100;
    game.score = savedGame.score || 0;

    //  You can use any of these from any function within this State.
    //  But do consider them as being 'reserved words', i.e. don't create a property for your own game called "world" or you'll over-write the world reference.
}

function commonCreate(game, playerX, playerY, levelName, nextLevel, levelStateName, songName){
    game.levelName = levelName;
    game.nextLevel = nextLevel;
    game.levelStateName = levelStateName;

    game.music = game.game.add.audio(songName);
    game.music.play();
    game.music.loopFull();

    game.game.physics.startSystem(Phaser.Physics.ARCADE);

    game.game.physics.arcade.gravity.y = 250;

    game.player = game.game.add.sprite(playerX, playerY, 'dude');
    game.player.stillHealing = false;
    game.game.physics.enable(game.player, Phaser.Physics.ARCADE);

    game.player.body.bounce.y = 0.2;
    game.player.body.collideWorldBounds = true;
    game.player.body.setSize(20, 32, 5, 16);

    game.player.animations.add('left', [0, 1, 2, 3], 10, true);
    game.player.animations.add('turn', [4], 20, true);
    game.player.animations.add('right', [5, 6, 7, 8], 10, true);

    game.game.camera.follow(game.player);

    game.cursors = game.game.input.keyboard.createCursorKeys();
    game.jumpButton = game.game.input.keyboard.addKey(Phaser.Keyboard.SPACEBAR);

    game.game.scale.fullScreenScaleMode = Phaser.ScaleManager.EXACT_FIT;
    //this.game.input.onDown.add(gofull, this);

    game.stars = game.game.add.group();
    game.stars.enableBody = true;

    game.baddies = game.game.add.group();
    game.baddies.enableBody = true;

    game.baddieMarkers = game.game.add.group();
    game.baddieMarkers.enableBody = true;

    game.diamonds = game.game.add.group();
    game.diamonds.enableBody = true;
}

function commonAfterCreate(game){
    game.scoreText = game.game.add.text(16, 16, 'Score: '+game.score, { fontSize: '22px', fill: '#FFF' });
    game.scoreText.fixedToCamera = true;
    game.scoreText.bringToTop();

    game.healthText = game.game.add.text(16, 35, 'Health: '+game.health, { fontSize: '22px', fill: '#FFF' });
    game.healthText.fixedToCamera = true;
    game.healthText.bringToTop();

    game.levelText = game.game.add.text(16, 54, game.levelName, { fontSize: '22px', fill: '#FFF' });
    game.levelText.fixedToCamera = true;
    game.levelText.bringToTop();
}

function commonUpdate(game){
    game.game.physics.arcade.collide(game.player, game.layer);
    game.game.physics.arcade.overlap(game.player, game.stars, function(player, star){
        collectStar(game, player, star);
    }, null, game);

    game.player.body.velocity.x = 0;

    if (game.cursors.left.isDown)
    {
        game.player.body.velocity.x = -150;

        if (game.facing != 'left')
        {
            game.player.animations.play('left');
            game.facing = 'left';
        }
    }
    else if (game.cursors.right.isDown)
    {
        game.player.body.velocity.x = 150;

        if (game.facing != 'right')
        {
            game.player.animations.play('right');
            game.facing = 'right';
        }
    }
    else
    {
        if (game.facing != 'idle')
        {
            game.player.animations.stop();

            if (game.facing == 'left')
            {
                game.player.frame = 0;
            }
            else
            {
                game.player.frame = 5;
            }

            game.facing = 'idle';
        }
    }
    
    if (game.jumpButton.isDown && game.player.body.onFloor() && game.game.time.now > game.jumpTimer)
    {
        var jumpSfx = game.game.add.audio('jump');
        jumpSfx.play();

        game.player.body.velocity.y = -250;
        game.jumpTimer = game.game.time.now + 750;
    }

    game.game.physics.arcade.collide(game.baddies, game.layer);
    game.game.physics.arcade.overlap(game.baddies, game.baddieMarkers, baddieMarkerTouched, null, game);
    game.baddies.children.forEach(function(baddie){
        if(!baddie.direction){
            baddie.direction = "left";
        }
        if(baddie.direction == "left"){
            baddie.body.velocity.x = -90;
            baddie.animations.play('left');
        }
        else if(baddie.direction == "right"){
            baddie.body.velocity.x = 90;
            baddie.animations.play('right');
        }
    });

    game.game.physics.arcade.overlap(game.baddies, game.player, function(player, baddie){
        playerTouchedBaddie(game, player, baddie);
    }, null, game);

    game.game.physics.arcade.overlap(game.player, game.diamonds, function(diamond, player){
        playerTouchDiamond(game, player, diamond);
    }, null, game);

    if(game.health <= 0){
        game.health = 100;
        game.music.stop();
        game.state.start(game.levelStateName);
    }
    else if(game.health >= 100){
        game.health = 100;
    }
}

function collectStar(game, player, star) {

    // Removes the star from the screen
    star.kill();

    //  Add and update the score
    addScore(game, 10);

    var coinSfx = game.game.add.audio('coin');
    coinSfx.play();
}

function addScore(game, scoreUp){
    game.score += scoreUp;
    game.scoreText.text = 'Score: ' + game.score;
}

function playerTouchedBaddie(game, player, baddie){
    if(baddie.body.touching.up){
        baddie.kill();
        addScore(game, 10);
        var stompFx = game.game.add.audio('stomp');
        stompFx.play();
    }
    else{
        if(!game.player.stillHealing){
            addHealth(game, -10);
            game.player.stillHealing = game;
            var self = game;
            setTimeout(function(){
                self.player.stillHealing = false;
            }, 1000);
        }
    }
}

function baddieMarkerTouched(baddie, marker){
    if(baddie.direction == "left"){
        baddie.direction = "right";
    }
    else{
        baddie.direction = "left";
    }
}

function addHealth(game, healthUp){
    game.health += healthUp;
    game.healthText.text = 'Health: '+game.health;
}

function playerTouchDiamond(game, player, diamond){
    if(!game.touchedDiamond){
        game.touchedDiamond = true;
        game.music.stop();
        var stageClearSfx = game.game.add.audio('stageClear');
        stageClearSfx.play();
        game.game.physics.arcade.isPaused = true;
        setTimeout(function(){
            var savedGame = {
                health: game.health,
                score: game.score,
                level: game.nextLevel
            };
            storage.set(savedGame);
            game.game.physics.arcade.isPaused = false;
            game.state.start(game.nextLevel);
        }, 3000);
    }
}

function initializeStars(game, name, id){
    game.map.createFromObjects(name, id, 'star', 0, true, false, game.stars);
    game.stars.children.forEach(function(star){
        star.body.velocity = 0;
    });
}

function initializeBaddies(game, name, id){
    game.map.createFromObjects(name, id, 'baddie1', 0, true, false, game.baddies);
    game.baddies.children.forEach(function(baddie){
        baddie.animations.add('left', [0, 1], 6, true);
        baddie.animations.add('right', [2, 3], 6, true);
    });
}

function initializeBaddieMarkers(game, name, id){
    game.map.createFromObjects(name, id, 'baddieMarker', 0, true, false, game.baddieMarkers);
    game.baddieMarkers.children.forEach(function(marker){
        marker.alpha = 0;
        marker.body.velocity = 0;
    });
}

function initializeDiamonds(game, name, id){
    game.map.createFromObjects(name, id, 'diamond', 0, true, false, game.diamonds);
    game.diamonds.children.forEach(function(marker){
        marker.body.velocity = 0;
    });
}