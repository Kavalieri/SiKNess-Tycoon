namespace SiKNessTycoon.Core
{
    /// <summary>
    /// Library of microtexts with "gamberro ligero" (light cheeky) tone.
    /// Used throughout the game for personality and player engagement.
    /// </summary>
    public static class MicrotextLibrary
    {
        #region Bottleneck Messages

        public static string GetBottleneckMessage(BottleneckType type)
        {
            return type switch
            {
                BottleneckType.Kitchen => "La cocina va a pedales, mete chispa 🔥",
                BottleneckType.Service => "La sala se arrastra... ¿les damos marcha? 😅",
                BottleneckType.Cashier => "La caja petardea, toca afinar ahí 💰",
                BottleneckType.None => "Todo controlado, jefe 😎",
                _ => "Algo raro pasa aquí..."
            };
        }

        #endregion

        #region AFK Messages

        public static string GetAFKMessage(float hours)
        {
            int h = UnityEngine.Mathf.FloorToInt(hours);
            int m = UnityEngine.Mathf.FloorToInt((hours - h) * 60);

            if (hours < 1)
            {
                return $"Tu gente ha tirado del carro 💪 Has rascado {m}m de curro fresquito.";
            }
            else if (hours < 4)
            {
                return $"Tu gente ha tirado del carro 💪 Has rascado {h}h{m:00}m de curro fresquito.";
            }
            else if (hours < 12)
            {
                return $"Buen turno sin ti, jefe 👌 Has rascado {h}h{m:00}m de producción.";
            }
            else
            {
                return $"Esto ha tirado solo 🚀 Has rascado {h}h{m:00}m. El bar va fino.";
            }
        }

        #endregion

        #region Upgrade Messages

        public static string GetUpgradeMessage(string upgradeName)
        {
            return upgradeName switch
            {
                "Marcha de fogones" => "Bien ahí 👌 La cocina ya no se arrastra",
                "Ritmo de barra" => "Ahora sí, van que vuelan los platos 🏃",
                "Ojo al stock" => "Ya no nos pilla el toro con el almacén 📦",
                "Mano de chef" => "Sale fino-fino ahora, nota el nivel 🍽️",
                "Sala ninja" => "Parecen teletransportados, esto vuela ⚡",
                "Voz del local" => "La gente sale hablando bien, se nota 💬",
                _ => $"Bien ahí 👌 {upgradeName} — Esto va fino ya"
            };
        }

        #endregion

        #region Staff Messages

        public static string GetHireMessage(string employeeName)
        {
            return $"{employeeName} se une al equipo 🤝 Dale caña, que lo peta.";
        }

        public static string GetFatigueMessage(string employeeName)
        {
            return $"{employeeName} está fundido 😴 Dale un respiro o va a petar.";
        }

        public static string GetOptimizeTurnsMessage()
        {
            return "Turnos optimizados 📋 Ahora esto tira como un tiro.";
        }

        #endregion

        #region Recipe Messages

        public static string GetRecipeUnlockedMessage(string recipeName)
        {
            return $"¡Nueva receta desbloqueada! {recipeName} 🍽️ A darle caña.";
        }

        public static string GetRecipeUpgradedMessage(string recipeName, int level)
        {
            if (level <= 3)
            {
                return $"{recipeName} sube de nivel 📈 Va mejorando la cosa.";
            }
            else if (level <= 5)
            {
                return $"{recipeName} nivel {level} 🔥 Esto ya pinta fino.";
            }
            else
            {
                return $"{recipeName} nivel {level} ⭐ Arte puro, jefe.";
            }
        }

        public static string GetSpecialDaySetMessage(string recipeName)
        {
            return $"Especial del día: {recipeName} 🌟 A por todas con este.";
        }

        #endregion

        #region Event Messages

        public static string GetWeeklyEventMessage(string eventName)
        {
            return eventName switch
            {
                "Semana del Picante" => "Semana del Picante 🌶️ La gente viene a por bravas, dale gas.",
                "Influencers Rondando" => "Influencers por aquí 📸 Cuida el servicio que nos pegan foto.",
                "Turno Motivado" => "Turno Motivado 🔥 El equipo hoy está on fire, aprovecha.",
                _ => $"{eventName} activo 📅 Hay movimiento especial esta semana."
            };
        }

        public static string GetMicroEventMessage(string eventName)
        {
            return eventName switch
            {
                "Racha Buena" => "Racha Buena 💫 Esto está que arde, aprovecha el tirón.",
                "Cliente VIP" => "Cliente VIP en sala 🎩 Si sale contento, llueve fama.",
                _ => $"{eventName} ⚡ Algo especial pasa ahora mismo."
            };
        }

        #endregion

        #region Venue Messages

        public static string GetVenueUnlockedMessage(string venueName)
        {
            return venueName switch
            {
                "Bistró Urbano" => "Bistró Urbano desbloqueado 🏙️ Ya somos referencia, jefe.",
                "Food Court" => "Food Court desbloqueado 🏢 Alto volumen, ritmo intenso. Vamos.",
                "Estación" => "Estación desbloqueada 🚉 Marca reconocible, esto es grande.",
                _ => $"{venueName} desbloqueado 🎉 Nueva sede, nuevo rollo."
            };
        }

        public static string GetVenueChangedMessage(string venueName)
        {
            return $"Cambiando a {venueName} 📍 Nuevo barrio, mismas manos.";
        }

        #endregion

        #region Objective Messages

        public static string GetObjectiveCompletedMessage(string objectiveName)
        {
            return $"Objetivo conseguido ✅ {objectiveName}. Bien ahí.";
        }

        public static string GetAllObjectivesCompletedMessage()
        {
            return "Todos los objetivos del día completados 🏆 Vas fino, jefe.";
        }

        #endregion

        #region Satisfaction Messages

        public static string GetSatisfactionMessage(float satisfactionLevel)
        {
            if (satisfactionLevel >= 0.9f)
            {
                return "La gente sale contentísima 😍 Esto va de lujo.";
            }
            else if (satisfactionLevel >= 0.7f)
            {
                return "Satisfacción alta 👍 La cosa tira bien.";
            }
            else if (satisfactionLevel >= 0.5f)
            {
                return "Va normal, pero podemos mejorar 🤔";
            }
            else if (satisfactionLevel >= 0.3f)
            {
                return "La gente sale regular 😐 Toca apretar un poco.";
            }
            else
            {
                return "Ojo, la gente sale mosqueada 😤 Hay que afinar ya.";
            }
        }

        #endregion

        #region General Actions

        public static string GetActionSuggestedMessage(string action, string area)
        {
            return action switch
            {
                "Mejorar" => $"Mejora {area} 🔧 Es el cuello ahora mismo.",
                "Contratar" => $"Contrata más gente en {area} 👥 Hace falta refuerzo.",
                "Optimizar" => $"Optimiza {area} 📊 Hay margen de mejora.",
                _ => $"Dale un vistazo a {area} 👀"
            };
        }

        public static string GetGenericSuccessMessage()
        {
            string[] messages = new string[]
            {
                "Bien ahí 👌",
                "Va fino 😎",
                "Así se hace 💪",
                "Dale caña 🔥",
                "Vamos bien 👍"
            };

            int index = UnityEngine.Random.Range(0, messages.Length);
            return messages[index];
        }

        #endregion
    }

    /// <summary>
    /// Enum for identifying bottleneck areas
    /// </summary>
    public enum BottleneckType
    {
        None,
        Kitchen,
        Service,
        Cashier
    }
}
