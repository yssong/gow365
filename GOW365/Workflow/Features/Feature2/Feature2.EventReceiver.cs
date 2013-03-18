using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Navigation;

namespace Workflow.Features.Feature2
{
    /// <summary>
    /// 이 클래스는 기능을 활성화, 비활성화, 설치, 제거 및 업그레이드하는 동안 발생하는 이벤트를 처리합니다.
    /// </summary>
    /// <remarks>
    /// 패키지하는 동안 이 클래스에 연결된 GUID를 사용할 수 있으며 수정하지 않아야 합니다.
    /// </remarks>

    [Guid("46907844-9473-40c7-bac1-2acc2eac13e1")]
    public class Feature2EventReceiver : SPFeatureReceiver
    {
        // 기능이 활성화된 후에 발생하는 이벤트를 처리하려면 아래 메서드의 주석 처리를 제거합니다.

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            using (SPWeb web = (SPWeb)properties.Feature.Parent)
            {
                SPNavigation nav = web.Navigation;
                foreach (SPNavigationNode node in nav.QuickLaunch)
                {
                    node.Delete();
                }
                SPNavigationNode processNode = new SPNavigationNode("결재함", web.ServerRelativeUrl);
                nav.QuickLaunch.AddAsFirst(processNode);
                SPNavigationNode processChildNode1 = new SPNavigationNode("결재대기문서", web.ServerRelativeUrl + "/Lists/eApproval/view.aspx");
                processNode.Children.AddAsLast(processChildNode1);
                //nav.QuickLaunch.Add(processChildNode1, processNode);
                SPNavigationNode processChildNode2 = new SPNavigationNode("결재진행문서", web.ServerRelativeUrl + "/Lists/eApproval/view1.aspx");
                processNode.Children.AddAsLast(processChildNode2);
                //nav.QuickLaunch.Add(processChildNode2, processNode);


                SPNavigationNode complateNode = new SPNavigationNode("문서함", web.ServerRelativeUrl);
                nav.QuickLaunch.AddAsLast(complateNode);

                SPNavigationNode complateChildNode1 = new SPNavigationNode("완료된 문서", web.ServerRelativeUrl + "/Lists/eApproval/view5.aspx");
                complateNode.Children.AddAsFirst(complateChildNode1);
                //nav.QuickLaunch.Add(complateChildNode1, complateNode);
                SPNavigationNode complateChildNode2 = new SPNavigationNode("기안한 문서", web.ServerRelativeUrl + "/Lists/eApproval/view2.aspx");
                complateNode.Children.AddAsFirst(complateChildNode2);
                //nav.QuickLaunch.Add(complateChildNode2, complateNode);
                SPNavigationNode complateChildNode3 = new SPNavigationNode("합의한 문서", web.ServerRelativeUrl + "/Lists/eApproval/view6.aspx");
                complateNode.Children.AddAsLast(complateChildNode3);
                //nav.QuickLaunch.Add(complateChildNode3, complateNode);
                SPNavigationNode complateChildNode4 = new SPNavigationNode("참조한 문서", web.ServerRelativeUrl + "/Lists/eApproval/view7.aspx");
                complateNode.Children.AddAsLast(complateChildNode4);
                //nav.QuickLaunch.Add(complateChildNode4, complateNode);
                SPNavigationNode complateChildNode5 = new SPNavigationNode("반려된 문서", web.ServerRelativeUrl + "/Lists/eApproval/view3.aspx");
                complateNode.Children.AddAsLast(complateChildNode5);
                //nav.QuickLaunch.Add(complateChildNode5, complateNode);
                SPNavigationNode complateChildNode6 = new SPNavigationNode("반려된 문서", web.ServerRelativeUrl + "/Lists/eApproval/view4.aspx");
                complateNode.Children.AddAsLast(complateChildNode6);
                //nav.QuickLaunch.Add(complateChildNode6, complateNode);

                SPNavigationNode adminNode = new SPNavigationNode("관리기능", web.ServerRelativeUrl);
                nav.QuickLaunch.AddAsLast(adminNode);
                SPNavigationNode adminChildNode1 = new SPNavigationNode("결재인장정보", web.ServerRelativeUrl + "/SignImage");
                adminNode.Children.AddAsLast(adminChildNode1);
                //nav.QuickLaunch.AddAsLast(adminChildNode1);
                SPNavigationNode adminChildNode2 = new SPNavigationNode("부서관리", web.ServerRelativeUrl + "/Lists/DeptInfo");
                adminNode.Children.AddAsLast(adminChildNode2);
                //nav.QuickLaunch.AddAsLast(adminChildNode2);
                SPNavigationNode adminChildNode3 = new SPNavigationNode("사용자관리", web.ServerRelativeUrl + "/Lists/OrgUsers");
                adminNode.Children.AddAsLast(adminChildNode3);
                //nav.QuickLaunch.AddAsLast(adminChildNode3);
                SPNavigationNode adminChildNode4 = new SPNavigationNode("문서템플릿", web.ServerRelativeUrl + "/Lists/eApprovalTemplate");
                adminNode.Children.AddAsLast(adminChildNode4);
                //nav.QuickLaunch.AddAsLast(adminChildNode4);


            }


        }


        // 기능이 비활성화되기 전에 발생하는 이벤트를 처리하려면 아래 메서드의 주석 처리를 제거합니다.

        //public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        //{
        //}


        // 기능이 설치된 후에 발생하는 이벤트를 처리하려면 아래 메서드의 주석 처리를 제거합니다.

        //public override void FeatureInstalled(SPFeatureReceiverProperties properties)
        //{
        //}


        // 기능이 제거되기 전에 발생하는 이벤트를 처리하려면 아래 메서드의 주석 처리를 제거합니다.

        //public override void FeatureUninstalling(SPFeatureReceiverProperties properties)
        //{
        //}

        // 기능이 업그레이드될 때 발생하는 이벤트를 처리하려면 아래 메서드의 주석 처리를 제거합니다.

        //public override void FeatureUpgrading(SPFeatureReceiverProperties properties, string upgradeActionName, System.Collections.Generic.IDictionary<string, string> parameters)
        //{
        //}
    }
}
