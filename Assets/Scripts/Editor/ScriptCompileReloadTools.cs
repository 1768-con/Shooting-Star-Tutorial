using UnityEditor;
using UnityEditor.Compilation;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using UnityEngine;
/// <summary>
/// ____DESC:      
/// </summary>
public class ScriptCompileReloadTools {
    /*
     * ���������� https://docs.unity.cn/cn/2021.3/Manual/DomainReloading.html
     * EditorApplication.LockReloadAssemblies()�� EditorApplication.UnlockReloadAssemblies() ��óɶ�
     * �����С��LockReloadAssemblies3�� ����ֻUnlockReloadAssemblies��һ�� ��ô���ǲ������� ����ҲҪ����ֻUnlockReloadAssemblies3��
     */
    static Stopwatch compileSW = new Stopwatch();

    const string menuEnableManualReloadDomain = "Tools/�����ֶ�Reload Domain";
    const string menuDisenableManualReloadDomain = "Tools/�ر��ֶ�Reload Domain";
    const string menuRealodDomain = "Tools/Unlock Reload %t";

    const string kManualReloadDomain = "ManualReloadDomain";

    static bool isEnterPlay;

    [InitializeOnLoadMethod]
    static void InitCompile() {
        //����Ҫ�������ע��
        CompilationPipeline.compilationStarted -= OncompilationStarted;
        CompilationPipeline.compilationStarted += OncompilationStarted;
        CompilationPipeline.compilationFinished -= OncompilationFinished;
        CompilationPipeline.compilationFinished += OncompilationFinished;

        //�༭������ projectsetting->editor->enterPlayModeSetting
        EditorSettings.enterPlayModeOptionsEnabled = true;
        EditorSettings.enterPlayModeOptions = EnterPlayModeOptions.DisableDomainReload;

        if(PlayerPrefs.HasKey(kManualReloadDomain)) {
            //�Ѿ������ֶ�
            bool isEnable = PlayerPrefs.GetInt(kManualReloadDomain, -1) == 1;

            Menu.SetChecked(menuEnableManualReloadDomain, isEnable ? false : true);
            Menu.SetChecked(menuDisenableManualReloadDomain, isEnable ? true : false);
        }
    }
    //���벥��ģʽ
    [InitializeOnEnterPlayMode]
    static void OnEnterPlayMode() {
        isEnterPlay = true;
    }

    //����ʼ�༭�ű�
    private static void OncompilationStarted(object obj) {
        compileSW.Start();
        Debug.Log("<color=yellow>��ʼ����ű�</color>");
    }
    //��������
    private static void OncompilationFinished(object obj) {
        compileSW.Stop();
        Debug.Log($"<color=yellow>��������ű� ��ʱ:{compileSW.ElapsedMilliseconds} ms</color>");
        compileSW.Reset();
    }


    //�����ֶ�reload
    [MenuItem(menuEnableManualReloadDomain)]
    static void EnableManualReloadDomain() {
        Menu.SetChecked(menuEnableManualReloadDomain, true);
        Menu.SetChecked(menuDisenableManualReloadDomain, false);

        PlayerPrefs.SetInt(kManualReloadDomain, 1);
        Debug.Log("<color=cyan>�����ֶ� Reload Domain</color>");

        EditorApplication.LockReloadAssemblies();
    }
    //�ر��ֶ�reload
    [MenuItem(menuDisenableManualReloadDomain)]
    static void DisenableManualReloadDomain() {
        Menu.SetChecked(menuEnableManualReloadDomain, false);
        Menu.SetChecked(menuDisenableManualReloadDomain, true);

        PlayerPrefs.SetInt(kManualReloadDomain, 0);

        Debug.Log("<color=cyan>�ر��ֶ� Reload Domain</color>");
        EditorApplication.UnlockReloadAssemblies();
        EditorApplication.UnlockReloadAssemblies();
        EditorApplication.UnlockReloadAssemblies();
    }
    //�ֶ�ˢ��
    [MenuItem(menuRealodDomain)]
    static void ManualReload() {
        if(PlayerPrefs.GetInt(kManualReloadDomain, -1) == 1) {
            Debug.Log("Reload Domain ......");
            EditorApplication.UnlockReloadAssemblies();
            EditorUtility.RequestScriptReload();
        }
    }

    //��Reload Domain
    [UnityEditor.Callbacks.DidReloadScripts]
    static void OnReloadDomain() {
        //������벥��ģʽ�� �Զ�reload ������
        if(isEnterPlay)
            return;
        //����֮���ٴ���ס
        if(PlayerPrefs.GetInt(kManualReloadDomain, -1) == 1) {
            EditorApplication.LockReloadAssemblies();
            Debug.Log("RealodDomain ���");
        }
    }
}