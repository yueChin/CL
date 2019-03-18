

public class SkillFactory  {

    public BaseSkill GetSkill(int price) 
    {
        return new BaseSkill(price);
    }

    public BattleSkill GetBattleSkill(int price)
    {
        return new BattleSkill(price);
    }
}
