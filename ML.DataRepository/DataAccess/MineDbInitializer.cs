using System.Collections.Generic;
using System.Data.Entity;
using ML.DataRepository.Models;

namespace ML.DataRepository.DataAccess
{
    public class MineDbInitializer : DropCreateDatabaseIfModelChanges<MineContext>
    {
        protected override void Seed(MineContext context)
        {
            if (context == null)
                context = new MineContext();

            //Init MainDigitSignalType table
            var MainDigitSignalType = new List<MainDigitSignalType>
                {
                    new MainDigitSignalType {Type = "ТП взведён"},
                    new MainDigitSignalType {Type = "Переподъём ФПК Н"},
                    new MainDigitSignalType {Type = "Переподъём ДКПУ Н"},
                    new MainDigitSignalType {Type = "Износ колодок"},
                    new MainDigitSignalType {Type = "Переподъём ФПК В"},
                    new MainDigitSignalType {Type = "Переподъём ДКПУ В"},
                    new MainDigitSignalType {Type = "ВВ включён"},
                    new MainDigitSignalType {Type = "ВАТ включён"},
                    new MainDigitSignalType {Type = "Контроль ОСЭРП"},
                    new MainDigitSignalType {Type = "Превышение скорости ОСЭРП"},
                    new MainDigitSignalType {Type = "Контроль растормаж машины"},
                    new MainDigitSignalType {Type = "Превышение скорости ФПК и ОС"},
                    new MainDigitSignalType {Type = "Обратныйход"},
                    new MainDigitSignalType {Type = "Контроль выбора схемы"},
                    new MainDigitSignalType {Type = "Контроль выбора режима"},
                    new MainDigitSignalType {Type = "Контроль АШСС и С"},
                    new MainDigitSignalType {Type = "Контроль затормаж машины"},
                    new MainDigitSignalType {Type = "Контроль" + @"""0""" + "СКАР"},
                    new MainDigitSignalType {Type = "Контроль" + @"""0""" + "ВБТР"},
                    new MainDigitSignalType {Type = "Контроль кнопки взвода ТП"},
                    new MainDigitSignalType {Type = "Готовность преобраз-ля"},
                    new MainDigitSignalType {Type = "ТР взведен"},
                    new MainDigitSignalType {Type = "Контроль давления ТП"},
                    new MainDigitSignalType {Type = "Запрет пуска"},

                };
            MainDigitSignalType.ForEach(d => context.MainDigitSignalType.Add(d));

            //Init IOsignalState table
            var MainDigitSignalState = new List<MainDigitSignalState>
                {
                    new MainDigitSignalState {State = "норма"},
                    new MainDigitSignalState {State = "авария"}
                };
            MainDigitSignalState.ForEach(d => context.MainDigitSignalState.Add(d));

            //Init AnalogSignalType table
            var analogSignalType = new List<AnalogSignal>
                {
                    new AnalogSignal {Type = "Путь s1(м)"},
                    new AnalogSignal {Type = "Путь s2(м)"},
                    new AnalogSignal {Type = "Скорость V(м/с)"},
                    new AnalogSignal {Type = "Ускорение (м/с2)"},
                    new AnalogSignal {Type = "Ток якоря (кА)"},
                    new AnalogSignal {Type = "Ток возбуждения (А)"},
                    new AnalogSignal {Type = "Защитная диаграмма (м/с)"}
                };
            analogSignalType.ForEach(s => context.AnalogSignals.Add(s));

            //Init GeneralLogType table
            var generalLogType = new List<GeneralLogType>
                {
                    new GeneralLogType {Type = "информационное"},
                    new GeneralLogType {Type = "предупредительное"},
                    new GeneralLogType {Type = "аварийное"}
                };
            generalLogType.ForEach(l => context.GeneralLogType.Add(l));

            context.SaveChanges();
        }
    }
}
