﻿#pragma checksum "..\..\DoOrder.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "E4752BC767DDDFF2311840087145B11984D3A923"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using KHEV2019;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace KHEV2019 {
    
    
    /// <summary>
    /// DoOrder
    /// </summary>
    public partial class DoOrder : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\DoOrder.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock UserText;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\DoOrder.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ComboAddress;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\DoOrder.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ComboDeliv;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\DoOrder.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ComboPay;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\DoOrder.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker DeliveryDate;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\DoOrder.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label cardLabel;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\DoOrder.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TCostd;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\DoOrder.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker DateOrder;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\DoOrder.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Order_num;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\DoOrder.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ComboCard;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\DoOrder.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock StatusText;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\DoOrder.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Add_Card;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/KHEV2019;component/doorder.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\DoOrder.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.UserText = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            
            #line 13 "..\..\DoOrder.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_1);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 14 "..\..\DoOrder.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.ComboAddress = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            this.ComboDeliv = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.ComboPay = ((System.Windows.Controls.ComboBox)(target));
            
            #line 17 "..\..\DoOrder.xaml"
            this.ComboPay.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ComboPay_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.DeliveryDate = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 8:
            this.cardLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 9:
            this.TCostd = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 10:
            this.DateOrder = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 11:
            this.Order_num = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 12:
            this.ComboCard = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 13:
            this.StatusText = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 14:
            this.Add_Card = ((System.Windows.Controls.Button)(target));
            
            #line 39 "..\..\DoOrder.xaml"
            this.Add_Card.Click += new System.Windows.RoutedEventHandler(this.Add_Card_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

