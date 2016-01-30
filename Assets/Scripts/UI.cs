using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI : MonoBehaviour {

    public CanvasGroup xpBarGroup;
    public Image xpBarFill;
    public CanvasGroup flash;
    public float fillSpeed;
    public float fadeSpeed;
    public float showTime;

    float showCounter = 0f;
    bool showing = false;

    float targetFill;

    void Start()
    {
        PlayerController.e_gemGet += HandleGemGet;
    }

    void HandleGemGet(int xp, int target)
    {
        targetFill = (float)xp / target;
        showing = true;
    }

    void Update()
    {
        if (showing)
        {
            xpBarGroup.alpha = 1f;
            showCounter += Time.deltaTime;

            if(showCounter >= showTime)
            {
                showCounter = 0f;
                showing = false;
            }
        }
        else
        {
            xpBarGroup.alpha = Mathf.MoveTowards(xpBarGroup.alpha, 0f, fadeSpeed * Time.deltaTime);
        }

        flash.alpha = Mathf.MoveTowards(flash.alpha, 0f, fadeSpeed * Time.deltaTime);
        xpBarFill.fillAmount = Mathf.MoveTowards(xpBarFill.fillAmount, targetFill, fillSpeed * Time.deltaTime);

        if(xpBarFill.fillAmount == 1f)
        {
            //LEVEL UP
            flash.alpha = 1f;
            xpBarFill.fillAmount = 0f;
            targetFill = 0f;
            PlayerController.instance.LevelUp();
        }
    }
}
