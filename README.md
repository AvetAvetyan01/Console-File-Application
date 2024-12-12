# Console-File-Application
This is a console application that outputs to a file a list of IP addresses from a text file that fall within a specified range, with the number of accesses to that address over a specified time interval. The data format is "IPv4:yyyy.MM.dd HH:mm:ss".

# Instruction (EN, RU)

English `

The following parameters are available `
  file-log - path to the file with logs.
  file-output - path to the file with the result.
  address-start - lower boundary of the address range, optional parameter,     all addresses are processed by default.
  address-mask - subnet mask, which defines the upper boundary of the    address range, decimal number. Optional parameter. If it is not specified,   all addresses starting from the lower boundary of the address range are processed. The parameter cannot be used if address-start is not specified.
  time-start - lower boundary of the time interval
  time-end - upper boundary of the time interval.

The following rules must be observed when transferring data from the configuration file `
  1. be sure to specify all parameters and all their names (if you do not want to specify the parameters "address-start" and "address-end", make their values equal to 0).
  2. There should be no `blank lines, blank spaces, and invalid lines in the configuration or data transfer file. 
  3. The date in the parameters must be in the format dd.MM.yyyy.
  4. Incomplete parameters will not be accepted.
  5. Parameters and their values must be separated by a colon (:). 

File path without quotes (" ").
Work only with text files.
All data is validated and the programme will not run if at least one parameter is invalid.

Русский `

Имеются следующие параметры `
  file-log - путь к файлу с логами.
  file-output - путь к файлу с результатом.
  address-start - нижняя граница диапазона адресов, необязательный параметр, по умолчанию обрабатываются все адреса.
  address-mask - маска подсети, задающая верхнюю границу диапазона адресов, десятичное число. Необязательный параметр. Если он не   указан, обрабатываются все адреса, начиная с нижней границы диапазона. Параметр не может быть использован, если не указан   address-start.
  time-start - нижняя граница временного интервала
  time-end - верхняя граница временного интервала.

При передаче данных из конфигурационного файла необходимо соблюдать следующие правила `
  1. Обязательно укажите все параметры и все их имена (если вы не хотите указывать параметры "address-start" и "address-end", сделайте их значения равными 0).
  2. В конфигурационном файле или файле передачи данных не должно быть `пустых строк, пустых пробелов и недопустимых строк.
  3. Дата в параметрах должна быть в формате dd.MM.yyyy.
  4. Неполные параметры не принимаются.
  5. Параметры и их значения должны быть разделены двоеточием (:). 

Путь к файлу без кавычек (" ").
Работать только с текстовыми файлами.
Все данные проверяются, и программа не будет работать, если хотя бы один параметр недействителен.
