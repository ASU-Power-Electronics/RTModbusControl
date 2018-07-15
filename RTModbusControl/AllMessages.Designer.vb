﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On

Imports System

Namespace My.Resources
    
    'This class was auto-generated by the StronglyTypedResourceBuilder
    'class via a tool like ResGen or Visual Studio.
    'To add or remove a member, edit your .ResX file then rerun ResGen
    'with the /str option, or rebuild your VS project.
    '''<summary>
    '''  A strongly-typed resource class, for looking up localized strings, etc.
    '''</summary>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0"),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute()>  _
    Friend Class AllMessages
        
        Private Shared resourceMan As Global.System.Resources.ResourceManager
        
        Private Shared resourceCulture As Global.System.Globalization.CultureInfo
        
        <Global.System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")>  _
        Friend Sub New()
            MyBase.New
        End Sub
        
        '''<summary>
        '''  Returns the cached ResourceManager instance used by this class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend Shared ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
            Get
                If Object.ReferenceEquals(resourceMan, Nothing) Then
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("RTModbusControl.AllMessages", GetType(AllMessages).Assembly)
                    resourceMan = temp
                End If
                Return resourceMan
            End Get
        End Property
        
        '''<summary>
        '''  Overrides the current thread's CurrentUICulture property for all
        '''  resource lookups using this strongly typed resource class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend Shared Property Culture() As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set
                resourceCulture = value
            End Set
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Please enter an integer in [1, 9999] or a hexadecimal value in [0, 270E]..
        '''</summary>
        Friend Shared ReadOnly Property ErrorAddressInvalid() As String
            Get
                Return ResourceManager.GetString("ErrorAddressInvalid", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Invalid Address Number.
        '''</summary>
        Friend Shared ReadOnly Property ErrorAddressInvalidCaption() As String
            Get
                Return ResourceManager.GetString("ErrorAddressInvalidCaption", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Please enter an integer in [1, 9999] or a hexadecimal value in [0, 270E].  Alternatively, a range of such values may be entered, connected by a hyphen (-)..
        '''</summary>
        Friend Shared ReadOnly Property ErrorAddressRangeInvalid() As String
            Get
                Return ResourceManager.GetString("ErrorAddressRangeInvalid", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Please enter an integer in [1, 247] in the Device ID field..
        '''</summary>
        Friend Shared ReadOnly Property ErrorIDInvalid() As String
            Get
                Return ResourceManager.GetString("ErrorIDInvalid", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Device ID Invalid.
        '''</summary>
        Friend Shared ReadOnly Property ErrorIDInvalidCaption() As String
            Get
                Return ResourceManager.GetString("ErrorIDInvalidCaption", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Please enter an integer in [0, 255] in each IP Address field..
        '''</summary>
        Friend Shared ReadOnly Property ErrorIPInvalid() As String
            Get
                Return ResourceManager.GetString("ErrorIPInvalid", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to IP Address Invalid.
        '''</summary>
        Friend Shared ReadOnly Property ErrorIPInvalidCaption() As String
            Get
                Return ResourceManager.GetString("ErrorIPInvalidCaption", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Please enter a unique nickname for the connection in the Name field to distinguish it from any others..
        '''</summary>
        Friend Shared ReadOnly Property ErrorNameInvalid() As String
            Get
                Return ResourceManager.GetString("ErrorNameInvalid", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Name Invalid.
        '''</summary>
        Friend Shared ReadOnly Property ErrorNameInvalidCaption() As String
            Get
                Return ResourceManager.GetString("ErrorNameInvalidCaption", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Please enter an integer in [1, 65535] in the Port field..
        '''</summary>
        Friend Shared ReadOnly Property ErrorPortInvalid() As String
            Get
                Return ResourceManager.GetString("ErrorPortInvalid", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Port Invalid.
        '''</summary>
        Friend Shared ReadOnly Property ErrorPortInvalidCaption() As String
            Get
                Return ResourceManager.GetString("ErrorPortInvalidCaption", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to No connected devices, controls not available, returning to main form..
        '''</summary>
        Friend Shared ReadOnly Property NoConnections() As String
            Get
                Return ResourceManager.GetString("NoConnections", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to No Connections.
        '''</summary>
        Friend Shared ReadOnly Property NoConnectionsCaption() As String
            Get
                Return ResourceManager.GetString("NoConnectionsCaption", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to No connections to slave devices.  Please create a connection now..
        '''</summary>
        Friend Shared ReadOnly Property NoSlaveConnection() As String
            Get
                Return ResourceManager.GetString("NoSlaveConnection", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to No slave connections.
        '''</summary>
        Friend Shared ReadOnly Property NoSlaveConnectionCaption() As String
            Get
                Return ResourceManager.GetString("NoSlaveConnectionCaption", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Are you sure you want to remove this connection?.
        '''</summary>
        Friend Shared ReadOnly Property RemoveConnection() As String
            Get
                Return ResourceManager.GetString("RemoveConnection", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Confirm Connection Removal.
        '''</summary>
        Friend Shared ReadOnly Property RemoveConnectionCaption() As String
            Get
                Return ResourceManager.GetString("RemoveConnectionCaption", resourceCulture)
            End Get
        End Property
    End Class
End Namespace
