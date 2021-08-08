using UnityEditor;
using UnityEngine.UIElements;
using PlasticGui;

namespace Unity.PlasticSCM.Editor.UI.UIElements
{
    class ProgressControlsForDialogs :
        VisualElement,
        IProgressControls
    {
        internal class Data
        {
            internal bool IsWaitingAsyncResult;
            internal float ProgressPercent;
            internal string ProgressMessage;

            internal MessageType StatusType;
            internal string StatusMessage;

            internal void CopyInto(Data other)
            {
                other.IsWaitingAsyncResult = IsWaitingAsyncResult;
                other.ProgressPercent = ProgressPercent;
                other.ProgressMessage = ProgressMessage;
                other.StatusType = StatusType;
                other.StatusMessage = StatusMessage;
            }
        }

        internal Data ProgressData { get { return mData; } }

        internal void ForcedUpdateProgress()
        {
            if (mData.IsWaitingAsyncResult)
            {
                mUndefinedProgress.RemoveFromClassList("display-none");
                mLoadingSpinner.Start();
            }
            else
            {
                mUndefinedProgress.AddToClassList("display-none");
                mLoadingSpinner.Stop();
            }

            mStatusLabel.text = mData.StatusMessage;
            mProgressLabel.text = mData.ProgressMessage;
        }

        internal ProgressControlsForDialogs(
            VisualElement[] actionControls)
        {
            mActionControls = actionControls;

            InitializeLayoutAndStyles();

            BuildComponents();
        }

        internal void EnableActionControls(bool enable)
        {
            if (mActionControls != null)
                foreach (var control in mActionControls)
                    if (control != null)
                        control.SetEnabled(enable);
        }

        void IProgressControls.HideProgress()
        {
            EnableActionControls(true);

            mData.IsWaitingAsyncResult = false;
            mData.ProgressMessage = string.Empty;
            ForcedUpdateProgress();
        }

        void IProgressControls.ShowProgress(string message)
        {
            EnableActionControls(false);

            CleanStatusMessage(mData);

            mData.IsWaitingAsyncResult = true;
            mData.ProgressPercent = 0f;
            mData.ProgressMessage = message;
            ForcedUpdateProgress();
        }

        void IProgressControls.ShowError(string message)
        {
            mData.StatusMessage = message;
            mData.StatusType = MessageType.Error;
            ForcedUpdateProgress();
        }

        void IProgressControls.ShowNotification(string message)
        {
            mData.StatusMessage = message;
            mData.StatusType = MessageType.Info;
            ForcedUpdateProgress();
        }

        void IProgressControls.ShowSuccess(string message)
        {
            mData.StatusMessage = message;
            mData.StatusType = MessageType.Info;
            ForcedUpdateProgress();
        }

        void IProgressControls.ShowWarning(string message)
        {
            mData.StatusMessage = message;
            mData.StatusType = MessageType.Warning;
            ForcedUpdateProgress();
        }

        void BuildComponents()
        {
            mUndefinedProgress = this.Query<VisualElement>("UndefinedProgress").First();
            mProgressLabel = this.Query<Label>("Progress").First();
            mStatusLabel = this.Query<Label>("Status").First();

            mLoadingSpinner = new LoadingSpinner();
            mUndefinedProgress.Add(mLoadingSpinner);
        }

        void InitializeLayoutAndStyles()
        {
            this.LoadLayout(typeof(ProgressControlsForDialogs).Name);

            this.LoadStyle(typeof(ProgressControlsForDialogs).Name);
        }

        static void CleanStatusMessage(Data data)
        {
            data.StatusMessage = string.Empty;
            data.StatusType = MessageType.None;
        }

        Data mData = new Data();
        VisualElement mUndefinedProgress;
        Label mProgressLabel;
        Label mStatusLabel;
        VisualElement[] mActionControls;

        LoadingSpinner mLoadingSpinner;
    }
}