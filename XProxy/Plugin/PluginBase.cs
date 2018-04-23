using System;
using System.Collections.Generic;
using System.Text;
using XProxy.Config;
using System.Reflection;
using XProxy.Base;

namespace XProxy.Plugin
{
    /// <summary>
    /// ������ࡣ
    /// ����ֱ�Ӽ̳и�����ʵ�ֲ����
    /// </summary>
    public abstract class PluginBase : IPlugin
    {
        #region ����
        private PluginManager _Manager;
        /// <summary>
        /// ���������
        /// </summary>
        public virtual PluginManager Manager { get { return _Manager; } set { _Manager = value; } }

        /// <summary>
        /// д��־�¼�
        /// </summary>
        public virtual event WriteLogDelegate OnWriteLog;
        #endregion

        #region IPlugin ��Ա
        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <param name="manager">���������</param>
        public virtual void OnInit(PluginManager manager)
        {
            Manager = manager;
        }

        /// <summary>
        /// ��ʼ����
        /// </summary>
        /// <param name="listener">������</param>
        public virtual void OnListenerStart(Listener listener)
        {
        }

        /// <summary>
        /// ֹͣ����
        /// </summary>
        /// <param name="listener">������</param>
        public virtual void OnListenerStop(Listener listener)
        {
        }

        /// <summary>
        /// ��һ����ͻ��˷�����ʱ������
        /// </summary>
        /// <param name="session">�ͻ���</param>
        /// <returns>�Ƿ�����ͨ��</returns>
        public virtual bool OnClientStart(Session session)
        {
            return true;
        }

        /// <summary>
        /// ����Զ�̷�����ʱ������
        /// </summary>
        /// <param name="session">�ͻ���</param>
        /// <returns>�Ƿ�����ͨ��</returns>
        public virtual bool OnServerStart(Session session)
        {
            return true;
        }

        /// <summary>
        /// �ͻ����������������ʱ������
        /// </summary>
        /// <param name="session">�ͻ���</param>
        /// <param name="Data">����</param>
        /// <returns>���������������</returns>
        public virtual byte[] OnClientToServer(Session session, byte[] Data)
        {
            return Data;
        }

        /// <summary>
        /// ��������ͻ��˷�����ʱ������
        /// </summary>
        /// <param name="session">�ͻ���</param>
        /// <param name="Data">����</param>
        /// <returns>���������������</returns>
        public virtual byte[] OnServerToClient(Session session, byte[] Data)
        {
            return Data;
        }

        private PluginConfig _Config;
        /// <summary>
        /// ��ǰ����
        /// </summary>
        public PluginConfig Config { get { return _Config; } set { _Config = value; } }

        /// <summary>
        /// Ĭ������
        /// </summary>
        public virtual PluginConfig DefaultConfig
        {
            get
            {
                PluginConfig pc = new PluginConfig();
                //pc.Name = "δ�������";
				pc.Name = this.GetType().Name;
				pc.Author = "����";
                pc.ClassName = this.GetType().FullName;
				//������ڲ��������ֻ��ʾ������������ʾȫ��
				if (Assembly.GetExecutingAssembly() == this.GetType().Assembly)
					pc.ClassName = this.GetType().Name;
				pc.Version = this.GetType().Assembly.GetName().Version.ToString();
				pc.Path = System.IO.Path.GetFileName(this.GetType().Assembly.Location);
				return pc;
            }
        }

        #endregion

        #region IDisposable ��Ա
        /// <summary>
        /// �ͷ���Դ
        /// </summary>
        public virtual void Dispose()
        {
        }
        #endregion

        #region ��־
        /// <summary>
        /// д��־
        /// </summary>
        /// <param name="msg">��־</param>
        public virtual void WriteLog(String msg)
        {
            if (OnWriteLog != null)
            {
                OnWriteLog(msg);
            }
        }
        #endregion

        /// <summary>
        /// �����ء�
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Config == null ? base.ToString() : Config.ToString();
        }
    }
}