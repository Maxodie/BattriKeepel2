using System.Collections.Generic;
using System;
using UnityEngine.Events;

using EntityID = System.Int32;

class EntityManager
{
    UnityEvent<IGameEntity> OnEntityAddedCallback;
    UnityEvent<Type> OnEntityDestroyedCallback;
    List<IGameEntity> m_loadedEntities;
    uint persistentDataOffset = 0;
    List<bool> m_loadedEntitiesActiveState;

    static EntityManager s_entityManager;

    public static EntityManager GetInstance()
    {
        if(s_entityManager == null)
        {
            s_entityManager = new();
        }

        return s_entityManager;
    }

    public EntityID CreatePersistentEntity<TEntity>(params object[] variadicArgs) where TEntity : IGameEntity
    {
        persistentDataOffset++;
        /*for()*/
        return CreateEntity<TEntity>(variadicArgs);
    }

    public EntityID CreateEntity<TEntity>(params object[] variadicArgs) where TEntity : IGameEntity
    {
        EntityID entityID;
        int deletedEntityID = m_loadedEntitiesActiveState.FindIndex((bool state) => { return state == false;});
        if(deletedEntityID == -1)
        {
            TEntity newEntity = (TEntity)Activator.CreateInstance(typeof(TEntity), args:variadicArgs);
            m_loadedEntities.Add(newEntity);
            m_loadedEntitiesActiveState.Add(true);

            entityID = m_loadedEntities.Count - 1;
        }
        else
        {
            TEntity newEntity = (TEntity)Activator.CreateInstance(typeof(TEntity), args:variadicArgs);
            m_loadedEntities[deletedEntityID] = newEntity;
            m_loadedEntitiesActiveState[deletedEntityID] = true;
            entityID = deletedEntityID;
        }

        OnEntityAddedCallback.Invoke(m_loadedEntities[entityID]);

        return entityID;
    }

    public bool IsEntityExisting(EntityID id)
    {
        return m_loadedEntitiesActiveState.Count < id && m_loadedEntitiesActiveState[id];
    }

    public void DestroyEntity(EntityID id)
    {
        if(IsEntityExisting(id))
        {
            OnEntityDestroyedCallback.Invoke(m_loadedEntities[id].GetType());
            m_loadedEntities[id] = null;
            m_loadedEntitiesActiveState[id] = false;
        }
    }

    public TEntity GetEntity<TEntity>(EntityID id) where TEntity : IGameEntity
    {
        if(IsEntityExisting(id))
        {
            return (TEntity)m_loadedEntities[id];
        }

        return default;
    }
}
