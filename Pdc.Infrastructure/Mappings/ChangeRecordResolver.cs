// Créez d'abord un resolver personnalisé
using AutoMapper;
using Pdc.Infrastructure.Entities.Versioning;

public class ChangeRecordResolver : IValueResolver<object, object, ChangeRecordEntity?>
{
    public ChangeRecordEntity? Resolve(object source, object destination, ChangeRecordEntity? destMember, ResolutionContext context)
    {
        // Détecter le type de l'objet source et extraire la Competency
        if (source is ChangeRecordEntity changeRecordEntity && changeRecordEntity != null)
        {
            var key = $"{changeRecordEntity.Id}";

            // Cache it
            if (context.Items.TryGetValue("EntityCache", out var cache))
            {
                var entityCache = (Dictionary<string, object>)cache;
                if (entityCache.TryGetValue(key, out var existingEntity))
                    return (ChangeRecordEntity)existingEntity;

                var mapped = context.Mapper.Map<ChangeRecordEntity>(changeRecordEntity);
                entityCache[key] = mapped;
                return mapped;
            }

            // Fallback au mapping standard si pas de cache
            return context.Mapper.Map<ChangeRecordEntity>(changeRecordEntity);
        }
        return null;
    }
}

//Proposé par Claude si jamais.....
//using AutoMapper;
//using Pdc.Infrastructure.Entities.Versioning;
//using Pdc.Domain.Models.Versioning; // Ajoutez l'import du modèle de domaine

//public class ChangeRecordResolver : IValueResolver<object, object, ChangeRecordEntity?>
//{
//    public ChangeRecordEntity? Resolve(object source, object destination, ChangeRecordEntity? destMember, ResolutionContext context)
//    {
//        // Extraire le ChangeRecord selon le type de source
//        ChangeRecord sourceChangeRecord = null;

//        // Déterminer quel type de source nous avons et extraire le ChangeRecord
//        if (source.GetType().GetProperty("ChangeRecord") != null)
//        {
//            sourceChangeRecord = source.GetType().GetProperty("ChangeRecord").GetValue(source) as ChangeRecord;
//        }

//        if (sourceChangeRecord == null)
//            return null;

//        // Créer une clé unique basée sur l'ID
//        var key = $"ChangeRecord_{sourceChangeRecord.Id}";

//        // Vérifier dans le cache
//        if (!context.Items.ContainsKey("EntityCache"))
//            context.Items["EntityCache"] = new Dictionary<string, object>();

//        var entityCache = (Dictionary<string, object>)context.Items["EntityCache"];

//        if (entityCache.TryGetValue(key, out var existingEntity))
//            return (ChangeRecordEntity)existingEntity;

//        // Si non trouvé, mapper et cacher
//        var mapped = context.Mapper.Map<ChangeRecordEntity>(sourceChangeRecord);
//        entityCache[key] = mapped;
//        return mapped;
//    }
//}