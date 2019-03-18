
public interface ITradeAcion  {
    void BuyGoodLoseCoin(int price);
    void SellGoodGainCoin(int price);
    bool IsEnoughCoin(int price);
}
