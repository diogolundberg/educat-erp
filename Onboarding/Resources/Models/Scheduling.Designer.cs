﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace onboarding.Resources.Models {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Scheduling {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Scheduling() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("onboarding.Resources.Models.Scheduling", typeof(Scheduling).Assembly);
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
        ///   Looks up a localized string similar to Final.
        /// </summary>
        public static string EndAt {
            get {
                return ResourceManager.GetString("EndAt", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Intervalo.
        /// </summary>
        public static string Intervals {
            get {
                return ResourceManager.GetString("Intervals", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Código do período da matrícula.
        /// </summary>
        public static string OnboardingId {
            get {
                return ResourceManager.GetString("OnboardingId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Horário de final do agendamento.
        /// </summary>
        public static string ScheduleEndTime {
            get {
                return ResourceManager.GetString("ScheduleEndTime", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Horário de início do agendamento.
        /// </summary>
        public static string ScheduleStartTime {
            get {
                return ResourceManager.GetString("ScheduleStartTime", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Início.
        /// </summary>
        public static string StartAt {
            get {
                return ResourceManager.GetString("StartAt", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A data de início do agendamento tem que ser maior que a data final..
        /// </summary>
        public static string StartAtGreaterThanEndAt {
            get {
                return ResourceManager.GetString("StartAtGreaterThanEndAt", resourceCulture);
            }
        }
    }
}
