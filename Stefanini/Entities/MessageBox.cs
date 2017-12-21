using System;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.UI;

namespace Stefanini.Entities
{
    public class MessageBox
    {
        private static Hashtable m_executingPages = new Hashtable();

        private MessageBox() { }

        public static void Show(string sMessage)
        {
            if (!m_executingPages.Contains(HttpContext.Current.Handler))
            {
                Page executingPage = HttpContext.Current.Handler as Page;

                if (executingPage != null)
                {

                    Queue messageQueue = new Queue();

                    messageQueue.Enqueue(sMessage);
                    m_executingPages.Add(HttpContext.Current.Handler, messageQueue);

                    executingPage.Unload += new EventHandler(ExecutingPage_Unload);
                }
            }
            else
            {
                Queue queue = (Queue)m_executingPages[HttpContext.Current.Handler];
                queue.Enqueue(sMessage);
            }
        }

        private static void ExecutingPage_Unload(object sender, EventArgs e)
        {
            Queue queue = (Queue)m_executingPages[HttpContext.Current.Handler];

            if (queue != null)
            {
                StringBuilder sb = new StringBuilder();

                int iMsgCount = queue.Count;

                sb.Append("<script language=javascript>");

                string sMsg;
                while (iMsgCount-- > 0)
                {
                    sMsg = (string)queue.Dequeue();
                    sMsg = sMsg.Replace("\n", "\\n");
                    sMsg = sMsg.Replace("\"", "'");
                    sb.Append(@"alert( """ + sMsg + @""" );");
                }

                sb.Append(@"</script>");

                m_executingPages.Remove(HttpContext.Current.Handler);

                HttpContext.Current.Response.Write(sb.ToString());
            }
        }
    }
}