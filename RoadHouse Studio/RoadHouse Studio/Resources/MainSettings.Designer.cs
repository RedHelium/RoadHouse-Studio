﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RoadHouse_Studio.Resources {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.8.1.0")]
    internal sealed partial class MainSettings : global::System.Configuration.ApplicationSettingsBase {
        
        private static MainSettings defaultInstance = ((MainSettings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new MainSettings())));
        
        public static MainSettings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string PathToSamples {
            get {
                return ((string)(this["PathToSamples"]));
            }
            set {
                this["PathToSamples"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public float SamplesVolume {
            get {
                return ((float)(this["SamplesVolume"]));
            }
            set {
                this["SamplesVolume"] = value;
            }
        }
    }
}
