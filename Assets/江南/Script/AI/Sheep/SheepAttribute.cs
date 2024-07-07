
public class SheepAttribute : AttributeBase
{
    public float warnRange = 10;

    public bool isBitedByDog;
    private void Start()
    {
        speed = 15;
        warnRange = 6;
        isBitedByDog = false;
    }
    public void DeadAction(PlayerAttribute player)
    {
        //player.dog.isBiteSheep = false;
    }




}
