
public static class FactoryManager  {

    private static AssetFactory mAssetFactory;
    private static MapCubeFactory mMapCubeFactory;
    private static MapFactory mMapFactory;
    private static PlayerFactory mPlayerFactory;
    private static EffectFactory mEffectFactory;
    private static CreatureFactory mCreatureFactory;
    private static AdventureEventFactory mAdventureFactory;
    private static SlotFactory mSlotFactory;
    private static InventoryFactory mInventoryFactory;
    private static GoodsFactory mGoodsFactory;
    private static EventFactory mEventFactory;

    public static AssetFactory AssetFactory
    {
        get
        {
            if (mAssetFactory == null)
            {
                mAssetFactory = new AssetFactory();
            }
            return mAssetFactory;
        }
    }

    public static MapCubeFactory MapCubeFactory
    {
        get
        {
            if (mMapCubeFactory == null)
            {
                mMapCubeFactory = new MapCubeFactory();
            }
            return mMapCubeFactory; 
        }
    }

    public static PlayerFactory PlayerFactory
    {
        get
        {
            if (mPlayerFactory == null)
            {
                mPlayerFactory = new PlayerFactory();
            }
            return mPlayerFactory;
        }
    }

    public static MapFactory MapFactory
    {
        get
        {
            if (mMapFactory == null)
            {
                mMapFactory = new MapFactory();
            }
            return mMapFactory;
        }
    }

    public static EffectFactory EffectFactory
    {
        get
        {
            if (mEffectFactory == null)
            {
                mEffectFactory = new EffectFactory();
            }
            return mEffectFactory;
        }
    }

    public static CreatureFactory CreatureFactory
    {
        get
        {
            if (mCreatureFactory == null)
            {
                mCreatureFactory = new CreatureFactory();
            }
            return mCreatureFactory;
        }
    }

    public static AdventureEventFactory AdventureEventFactory
    {
        get
        {
            if (mAdventureFactory == null)
            {
                mAdventureFactory = new AdventureEventFactory();
            }
            return mAdventureFactory;
        }       
    }

    public static SlotFactory SlotFactory
    {
        get
        {
            if (mSlotFactory == null)
            {
                mSlotFactory = new SlotFactory();
            }
            return mSlotFactory;
        }
    }

    public static InventoryFactory InventoryFactory
    {
        get
        {
            if (mInventoryFactory == null)
            {
                mInventoryFactory = new InventoryFactory();
            }
            return mInventoryFactory;
        }
    }

    public static GoodsFactory GoodsFactory
    {
        get
        {
            if (mGoodsFactory == null)
            {
                mGoodsFactory = new GoodsFactory();
            }
            return mGoodsFactory;
        }
    }

    public static EventFactory EventFactory
    {
        get
        {
            if (mEventFactory == null)
            {
                mEventFactory = new EventFactory();
            }
            return mEventFactory;
        }
    }
}
