# Небольшой логгер для .NET Framework приложений
> Моя реализация библиотеки логгера для использования, чаще всего, с WinForms и .NET Framework приложениями
## Функционал
**Этот логгер сокращает колличество печатанья комманд предоставляя несколько гибко-используемых методов**
#### ```SetLogPath(string path)```
Устанавливает расположение того, куда будет записан файл.  
Если путь уже установлен, его невозможно установить снова.  
#### ```AddTimeStamp()```
Добавляет временную отметку в 24-часовом формате в начале строки.  
Если отметка уже была добавлена, её нельзя добавить снова.  
### Добавление информации в строку
Имеются 3 разных метода для добавления строки в лог.  
#### ```AddInfoToLine(string line)```
Добавляет ранее написанную строку и разделяет следующие строки ','.  
#### ```AddInfoToLineWithTime(string line)```
Добавляет ранее написанную строку, при этом добавляет временную отметку в начале строки, если она не была добавлена ранее, разделяя следующие строки ','.  
Если используются в середине строки методов, добавляет отметку в начало строки.
#### ```AddInfoToLineWithHeader(string header, string line, int type)```
Добавляет ранее написанную строку, добавляя заголовок вначале обозначающий последующую информацию, разделяя следующие строки ','.  
Имеет 2 стиля:  
```
1. Заголовок: Строка.
2. [Заголовок]: Строка.
```
### Добавление массива или списка
*Поддерживаются следующие типы массивов: object, string, int.*  
Позволяет добавлять массивы в строку, разделяя содержимое при помощи ';'.  
**Вы можете добавить массив в строку следующими способами:**  
- ```AddArray(type[] param)``` - добавить массив в строку.  
- ```AddArray(string header, type[] param)``` - добавить массив в строку с обозначающим его заголовком.  
### Добавление словаря
*На данный момент поддерживается только такой тип словаря: <string, string[]>.*  
Позволяет добавлять словари в строку в формате: Ключ:~ значени1; значени2; значени3 "*сдвиг на другую линию*" и продолжается в таком же духе.  
Как и в случае с массивами, вы можете добавлять словари как с заголовками, так и без них.  
### Добавление расширенной строки
```AddExtendedInfo()``` перемещает вас в класс, где вы можете добавить строку со слегка большей информацией.  
*Автоматически устанавливает временную отметку в начальне строки, если используется не в середине строки методов.*  
#### ```AddFromMethod(string methodName)```
Передайте сюда имя метода откуда исходит информация.  
Добавляет информацию в строку в формате [Method/имяМетода].  
Вы не можете добавить более одного 'из метода'.  
#### ```AddFromObject(Control obj)```
*Используется для WinForms*  
Передайте сюда объект с типом Control из которого исходит информация.  
Добавляте информацию в строку в формате [Object/имяОбъекта].  
Вы не можете добавить более одного 'из объекта'.  
#### ```AddHighlight(string light, string str)```
Вы можете добавлять своё собсвтенное выделение информации.  
Передайте сюда информацию, которую вы бы хотели выделить в формате [Родитель/Дочь].  
Несколько примеров применения:  
- [Имя класса/Имя метода]
- [Тип объекта/Имя объекта]
- [Имя метода/Часть метода]
#### Добавление информации в строку
Добавление иформации в строку ровно такое же, как оно показано в начале.  
Однако здесь отсутсвует добавление строки с временной отметкой, так как она сама добавляется в начале автоматически.
### Подтверждение строки
Чтобы переместиться на следующую строку, сначала нужно подтвердить редактируемую.  
Имеются два метода для подтверждения строки: 
- ```AcceptWholeLine()``` - регистрирует всю ранее редактируемую строку и смещает вас на другую строку. Может быть использовано внутри ```Расширенного редактирования``` для подтвреждения всей строки и перемещния на следующую.  
- ```AcceptDeepLine()``` - используется только внутри ```Расширенного редактирования```. Подтверждает расширенную строку и позволяет редактировать всю оставшуюся.  
### Создание лога
После того, как вы завершили всё редактирование и все строки подготовлены, вы можете ввести ```CreateLog(int mode)```, чтобы записать всю информацию в файл по пути отмеченному в методе ```SetLogPath```.  
"mode" означает то, как файл будет записан. Изначально режим установлен на 0, обозначающий ```FileMode.Append```, это добавляет строки в конец файла.  
1 обозначает ```FileMode.Create``, оно может переписать уже существующий файл. Полезно в тех случаях, когда вы не хотите делать киллометры текста в каком-нибудь из файлов.  
Замечания:
- Вы можете использовать ```CreateLog``` в ```Расширенном редактировании``` и в ```Обычном редактировании```  
- Вы можете использовать ```CreateLog``` даже если вы не подтвердили всю или расширенную строку. Оно записывает всю информацию автоматически.  
- Если вы не добавили ни одной строки с информацией, лог не будет записан.
