using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IntroGamePanel : BasePanel
{
    private TextMeshProUGUI Count;
    public override void Start()
    {
        base.Start();
        //查找并返回
        Count = SearchChildUI<TextMeshProUGUI>("目前消灭数量");
        //TODO:其实血条也可以都在这个脚本里做，来不及改了。





    }
    public override void OnClick(string name)
    {
        base.OnClick(name);
        // TODO:准备在这个panel页面做新手教程。。。。






    }

    private void OnEnable()
    {
        EventCenter.Instance.AddListener("更改目前敌人数量", OnKillEnemy);
    }
    private void OnDisable()
    {
        EventCenter.Instance.RemoveListener("更改目前敌人数量", OnKillEnemy);
    }

    private void OnKillEnemy(object num)
    {
        //都测一下，不确定哪个好使。。。
        //Count.GetComponent<TextMeshProUGUI>().text = num.ToString();
        //Debug.Log("文字内容更改为："+ num.ToString());
        Count.text = num.ToString();
    }

}
