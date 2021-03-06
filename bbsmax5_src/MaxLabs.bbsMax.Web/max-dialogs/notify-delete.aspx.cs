﻿//
// 请注意：bbsmax 不是一个免费产品，源代码仅限用于学习，禁止用于商业站点或者其他商业用途
// 如果您要将bbsmax用于商业用途，需要从官方购买商业授权，得到授权后可以基于源代码二次开发
//
// 版权所有 厦门麦斯网络科技有限公司
// 公司网站 www.bbsmax.com
//

using System;
using System.Collections;
using System.Web;

using MaxLabs.WebEngine;
using MaxLabs.bbsMax.Enums;
using MaxLabs.bbsMax.Errors;
using MaxLabs.bbsMax.Entities;


namespace MaxLabs.bbsMax.Web.max_dialogs
{
    public partial class notify_delete : DialogPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (_Request.IsClick("deletenotify"))
            //{
                DeleteNotify();
            //}
        }

        protected override bool EnableClientBuffer
        {
            get
            {
                return false;
            }
        }

        private void DeleteNotify()
        {
            using (new ErrorScope())
            {
                MessageDisplay msgDisplay = CreateMessageDisplay();

                int[] notifyIDs = _Request.GetList<int>("notifyid", Method.All, new int[0]);
                int notifyID = _Request.Get<int>("notifyid", Method.All, 0);
                bool success = false;

                if (notifyID < 0)
                {
                    SystemNotifyProvider.IgnoreNotify(MyUserID, notifyID);
                    success = true;
                }
                else
                {
                    try
                    {


                        if (NotifyBO.Instance.DeleteNotifies(MyUserID, notifyIDs))
                        {
                            success = true;
                        }
                        else
                        {
                            CatchError<ErrorInfo>(delegate(ErrorInfo error)
                            {
                                msgDisplay.AddError(error);
                            }
                            );
                        }
                    }
                    catch (Exception ex)
                    {
                        msgDisplay.AddError(ex.Message);
                    }
                }
                if (success)
                    Return(notifyIDs);
            }
        }
    }
}