using UnityEngine;

public class ActionQueueEntry : MonoBehaviour
{
    Enemy m_Enemy;
    PlayableCharacter m_PlayableCharacter;

    public ActionQueueEntry(Enemy a_Enemy, PlayableCharacter a_PlayableCharacter)
    {
        m_Enemy = a_Enemy;
        m_PlayableCharacter = a_PlayableCharacter;
    }

    public void ReInit(Enemy a_Enemy, PlayableCharacter a_PlayableCharacter)
    {
        m_Enemy = a_Enemy;
        m_PlayableCharacter = a_PlayableCharacter;
    }

    public Enemy GetEnemy()
    {
        return m_Enemy;
    }

    public Fighter GetEnemyFighter()
    {
        return m_Enemy.GetComponent<Fighter>();
    }

    public Fighter GetPCFighter()
    {
        if (m_PlayableCharacter != null)
        {
            return m_PlayableCharacter.GetComponent<Fighter>();
        }
        else
        {
            return null;
        }
    }
}
