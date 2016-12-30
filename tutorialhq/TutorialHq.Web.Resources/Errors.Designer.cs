﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TutorialHq.Web.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Errors {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Errors() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("TutorialHq.Web.Resources.Errors", typeof(Errors).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Argument error: {0}.
        /// </summary>
        public static string argumentError {
            get {
                return ResourceManager.GetString("argumentError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to you can&apos;t update this profile.
        /// </summary>
        public static string cantUpdateProfile {
            get {
                return ResourceManager.GetString("cantUpdateProfile", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to you can&apos;t vote on your own tutorials.
        /// </summary>
        public static string cantVoteOwnTutorials {
            get {
                return ResourceManager.GetString("cantVoteOwnTutorials", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Conflict detected: {0}.
        /// </summary>
        public static string conflictDetected {
            get {
                return ResourceManager.GetString("conflictDetected", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to JSON is corrupted.
        /// </summary>
        public static string corruptJson {
            get {
                return ResourceManager.GetString("corruptJson", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to the rating should be between 1 and 10.
        /// </summary>
        public static string ratingRange {
            get {
                return ResourceManager.GetString("ratingRange", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Resource not found: {0}.
        /// </summary>
        public static string resourceNotFound {
            get {
                return ResourceManager.GetString("resourceNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to do not request more than 50 tutorials at once.
        /// </summary>
        public static string tooManyRequests {
            get {
                return ResourceManager.GetString("tooManyRequests", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to not authorized.
        /// </summary>
        public static string unauthorized {
            get {
                return ResourceManager.GetString("unauthorized", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to you can vote only once in 24 hours.
        /// </summary>
        public static string voteLimit {
            get {
                return ResourceManager.GetString("voteLimit", resourceCulture);
            }
        }
    }
}