# DistibutedLab_proj1

Первое практическое задание выполнено. Если интересно можете посмотреть еще на два других моих алгоритма генерации битовых цепочек
(ветки tree-algoritm и first-variant, полностью задание в тех ветках не реализовано, лишь алгоритмы генерации, алгоритмы используют оперативную память
поэтому использование их на ключах длинной больше 16 бит невозможно, по этой причине пришлось искать другой выход и писать третий алгоритм). Для хранения огромных значений 
использовалась структура BigInteger. Задача поиска ключа выполняется асинхронно, что позволяет не замораживать главный поток при выполнении тяжелых задач, как например генерация
ключей длинной больше 16 бит.
