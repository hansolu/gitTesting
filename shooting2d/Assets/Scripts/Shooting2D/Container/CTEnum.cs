public class CTEnum
{
    public enum EnemyKind
    { 
        Enemy_A,
        Enemy_B,
        Enemy_C,
        //Boss,

        End //����. ���� for�� ������ �����ϰ� ��������.
    }
    public enum BulletKind
    { 
        PlayerBullet,
        //EnemyBullet_A,
        EnemyBullet_B,
        EnemyBullet_C,
            Boss,
            Player_Follower
    }
    public enum ItemKind 
    { 
        PowerUp,
        HPUp,
        //SpeedUp,        

        End
    }
    public enum ItemType
    { 
        AttackUP,
        HPup,
        Morph, 


    }
}