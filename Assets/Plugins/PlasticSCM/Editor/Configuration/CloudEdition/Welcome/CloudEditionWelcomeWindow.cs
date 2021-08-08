using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

using PlasticGui;
using PlasticGui.WebApi;
using Unity.PlasticSCM.Editor.UI.UIElements;
using PlasticGui.Configuration.CloudEdition.Welcome;
using System.Collections.Generic;
using Codice.Client.Common.Servers;
using Codice.Client.Common;
using Codice.Utils;

namespace Unity.PlasticSCM.Editor.Configuration.CloudEdition.Welcome
{
    internal interface IWelcomeWindowNotify
    {
        void SuccessForConfigure(List<string> organizations, bool canCreateAnOrganization);
        void SuccessForSSO(string organization);
        void SuccessForCredentials(string userName, string password);
        void SuccessForProfile(string userName);
        void SignUpNeeded(string user, string password);
        void Back();
    }

    internal class CloudEditionWelcomeWindow :
        EditorWindow,
        OAuthSignIn.INotify,
        IWelcomeWindowNotify
    {
        internal static void ShowWindow(
            IPlasticWebRestApi restApi,
            CmConnection cmConnection)
        {
            sRestApi = restApi;
            sCmConnection = cmConnection;

            CloudEditionWelcomeWindow window = GetWindow<CloudEditionWelcomeWindow>();

            window.titleContent = new GUIContent(
                PlasticLocalization.GetString(PlasticLocalization.Name.SignInToPlasticSCM));
            window.minSize = window.maxSize = new Vector2(800, 460);

            window.Show();
        }

        void OnEnable()
        {
            BuildComponents();
        }

        internal void JoinOrganization(string organization)
        {
            SaveCloudServer.ToPlasticGuiConfig(organization);
            SaveCloudServer.ToPlasticGuiConfigFile(
                organization, GetPlasticConfigFileToSaveOrganization());
            SaveCloudServer.ToPlasticGuiConfigFile(
                organization, GetGluonConfigFileToSaveOrganization());

            KnownServers.ServersFromCloud.InitializeForWindows(
                PlasticGuiConfig.Get().Configuration.DefaultCloudServer);

            CloudEditionWelcome.WriteToTokensConf(
                organization,
                mUserName,
                mAccessToken);

            GetWelcomePage.Run(sRestApi, organization);
        }

        internal void ReplaceRootPanel(VisualElement panel)
        {
            rootVisualElement.Clear();
            rootVisualElement.Add(panel);
        }

        void OnDestroy()
        {
            Dispose();
        }

        void Dispose()
        {
            if (mSignInPanel != null)
                mSignInPanel.Dispose();

            if (mSSOSignUpPanel != null)
                mSSOSignUpPanel.Dispose();

            if (mOrganizationPanel != null)
                mOrganizationPanel.Dispose();
        }

        void OAuthSignIn.INotify.SuccessForConfigure(
            List<string> organizations,
            bool canCreateAnOrganization,
            string userName,
            string accessToken)
        {
            ShowOrganizationPanel(
                GetWindowTitle(),
                organizations,
                canCreateAnOrganization);

            Focus();

            mUserName = userName;
            mAccessToken = accessToken;
        }

        internal void ShowOrganizationPanel(
            string title,
            List<string> organizations,
            bool canCreateAnOrganization)
        {
            mOrganizationPanel = new OrganizationPanel(
                this,
                sRestApi,
                title,
                organizations,
                canCreateAnOrganization);

            ReplaceRootPanel(mOrganizationPanel);
        }

        void OAuthSignIn.INotify.SuccessForSSO(string organization)
        {
            // empty implementation
        }

        void OAuthSignIn.INotify.SuccessForProfile(string email)
        {
            // empty implementation
        }

        void OAuthSignIn.INotify.SuccessForCredentials(
            string email,
            string accessToken)
        {
            // empty implementation
        }

        void OAuthSignIn.INotify.Cancel(string errorMessage)
        {
            Focus();
        }

        void IWelcomeWindowNotify.SuccessForConfigure(
            List<string> organizations,
            bool canCreateAnOrganization)
        {
            ShowOrganizationPanel(
                GetWindowTitle(),
                organizations,
                canCreateAnOrganization);
        }

        void IWelcomeWindowNotify.SuccessForSSO(string organization)
        {
            // empty implementation
        }

        void IWelcomeWindowNotify.SuccessForCredentials(string userName, string password)
        {
            // empty implementation
        }

        void IWelcomeWindowNotify.SuccessForProfile(string userName)
        {
            // empty implementation
        }

        void IWelcomeWindowNotify.SignUpNeeded(
            string user,
            string password)
        {
            SwitchToSignUpPage(user, password);
        }

        void IWelcomeWindowNotify.Back()
        {
            rootVisualElement.Clear();
            rootVisualElement.Add(mTabView);
        }

        void SwitchToSignUpPage(
            string user,
            string password)
        {
            mSSOSignUpPanel.SetSignUpData(user, password);

            rootVisualElement.Clear();
            rootVisualElement.Add(mTabView);
            mTabView.SwitchContent(mSSOSignUpPanel);
        }

        string GetWindowTitle()
        {
            return mIsOnSignIn ?
                PlasticLocalization.GetString(PlasticLocalization.Name.SignInToPlasticSCM) :
                PlasticLocalization.GetString(PlasticLocalization.Name.SignUp);
        }

        static string GetPlasticConfigFileToSaveOrganization()
        {
            if (PlatformIdentifier.IsMac())
            {
                return "macgui.conf";
            }

            return "plasticgui.conf";
        }

        static string GetGluonConfigFileToSaveOrganization()
        {
            if (PlatformIdentifier.IsMac())
            {
                return "gluon.conf";
            }

            return "gameui.conf";
        }

        void BuildComponents()
        {
            VisualElement root = rootVisualElement;

            root.Clear();
            mTabView = new TabView();

            mSignInPanel = new SignInPanel(
                this,
                sRestApi,
                sCmConnection);

            mSSOSignUpPanel = new SSOSignUpPanel(
                this,
                sRestApi,
                sCmConnection);

            mTabView.AddTab(
                PlasticLocalization.GetString(PlasticLocalization.Name.SignIn),
                mSignInPanel).clicked += () =>
                {
                    mIsOnSignIn = true;
                    titleContent = new GUIContent(GetWindowTitle());
                };
            mTabView.AddTab(
                PlasticLocalization.GetString(PlasticLocalization.Name.SignUp),
                mSSOSignUpPanel).clicked += () =>
                {
                    mIsOnSignIn = false;
                    titleContent = new GUIContent(GetWindowTitle());
                };

            root.Add(mTabView);
        }

        internal TabView mTabView;

        OrganizationPanel mOrganizationPanel;
        SignInPanel mSignInPanel;
        SSOSignUpPanel mSSOSignUpPanel;

        string mUserName;
        string mAccessToken;

        bool mIsOnSignIn = true;

        static IPlasticWebRestApi sRestApi;
        static CmConnection sCmConnection;
    }
}