public class SoundDialogBox : SoundBase
{
    public void TouchClick()
    {
        soundManager.TouchClick(1f);
    }

    public void DiaLogSoundBox()
    {
        soundManager.HelpDialogSound();
    }

    public void SnailGot()
    {
        soundManager.OcSenGot();
    }
}