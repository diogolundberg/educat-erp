﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace onboarding.Resources {
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
    public class Messages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Messages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("onboarding.Resources.Messages", typeof(Messages).Assembly);
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
        ///   Looks up a localized string similar to Horário já agendado..
        /// </summary>
        public static string AppointmentAlreadyUsed {
            get {
                return ResourceManager.GetString("AppointmentAlreadyUsed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Horário não existe..
        /// </summary>
        public static string AppointmentNotExisting {
            get {
                return ResourceManager.GetString("AppointmentNotExisting", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Estes dados já foram enviados para a análises e não pode ser editados no momento..
        /// </summary>
        public static string EnrollmentInReview {
            get {
                return ResourceManager.GetString("EnrollmentInReview", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Link para matrícula inválido..
        /// </summary>
        public static string EnrollmentLinkIsNotValid {
            get {
                return ResourceManager.GetString("EnrollmentLinkIsNotValid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Número de matrícula inválido..
        /// </summary>
        public static string EnrollmentNumberIsNotValid {
            get {
                return ResourceManager.GetString("EnrollmentNumberIsNotValid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Já existe um agendamento cadastrado para esse período de matrícula..
        /// </summary>
        public static string HaveSchedulingForOnboarding {
            get {
                return ResourceManager.GetString("HaveSchedulingForOnboarding", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Não é possível excluir o período pois existem matriculas iniciadas..
        /// </summary>
        public static string OnboardingDeleteBlock {
            get {
                return ResourceManager.GetString("OnboardingDeleteBlock", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Existem cpfs duplicados neste período de matrícula..
        /// </summary>
        public static string OnboardingDuplicateCpf {
            get {
                return ResourceManager.GetString("OnboardingDuplicateCpf", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Não é possível alterar o período pois existem matriculas iniciadas..
        /// </summary>
        public static string OnboardingEditBlock {
            get {
                return ResourceManager.GetString("OnboardingEditBlock", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Já existe um período de matrícula cadastrado para este semestre e ano..
        /// </summary>
        public static string OnboardingExisting {
            get {
                return ResourceManager.GetString("OnboardingExisting", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to O prazo para esta matrícula foi encerrado..
        /// </summary>
        public static string OnboardingExpired {
            get {
                return ResourceManager.GetString("OnboardingExpired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Esse período de matrícula não existe..
        /// </summary>
        public static string OnboardingNotExisting {
            get {
                return ResourceManager.GetString("OnboardingNotExisting", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Agendamento não existe..
        /// </summary>
        public static string SchedulingNotExisting {
            get {
                return ResourceManager.GetString("SchedulingNotExisting", resourceCulture);
            }
        }
    }
}
