using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;

namespace WinWebSolution.Module {

    public class Mycontroller : ViewController<ListView> {
        NewObjectViewController controller1;
        ListViewProcessCurrentObjectController controller2;

        public Mycontroller() {
            TargetViewNesting = Nesting.Nested;
            TargetObjectType = typeof(Transaction);
        }

        protected override void OnActivated() {
            base.OnActivated();
            controller1 = Frame.GetController<NewObjectViewController>();
            if (controller1 != null) {
                controller1.ObjectCreated += new EventHandler<ObjectCreatedEventArgs>(controller1_ObjectCreated);
            }
            controller2 = Frame.GetController<ListViewProcessCurrentObjectController>();
            if (controller2 != null) {
                controller2.CustomProcessSelectedItem += new EventHandler<CustomProcessListViewSelectedItemEventArgs>(controller2_CustomProcessSelectedItem);
            }
        }

        protected override void OnDeactivated() {
            if (controller1 != null) {
                controller1.ObjectCreated -= new EventHandler<ObjectCreatedEventArgs>(controller1_ObjectCreated);
            }
            if (controller2 != null) {
                controller2.CustomProcessSelectedItem -= new EventHandler<CustomProcessListViewSelectedItemEventArgs>(controller2_CustomProcessSelectedItem);
            }
            base.OnDeactivated();
        }

        void controller1_ObjectCreated(object sender, ObjectCreatedEventArgs e) {
            Application.DetailViewCreating += new EventHandler<DetailViewCreatingEventArgs>(Application_DetailViewCreating);
        }

        void controller2_CustomProcessSelectedItem(object sender, CustomProcessListViewSelectedItemEventArgs e) {
            Application.DetailViewCreating += new EventHandler<DetailViewCreatingEventArgs>(Application_DetailViewCreating);
        }

        void Application_DetailViewCreating(object sender, DetailViewCreatingEventArgs e) {
            ((XafApplication)sender).DetailViewCreating -= new EventHandler<DetailViewCreatingEventArgs>(Application_DetailViewCreating);
            if (e.Obj is Transaction && Frame is NestedFrame) {
                Type parentType = ((NestedFrame)Frame).ViewItem.ObjectType;
                if (typeof(Consumer).IsAssignableFrom(parentType)) {
                    e.ViewID = "Transaction_DetailView_FromConsumer";
                }
                if (typeof(Producer).IsAssignableFrom(parentType)) {
                    e.ViewID = "Transaction_DetailView_FromProducer";
                }
            }
        }
    }
}
