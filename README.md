# WhetherMysqlSham
# 404StarLink 2.0 - Galaxy
![](https://github.com/knownsec/404StarLink-Project/raw/master/logo.png)

WhetherMysqlSham 是 404Team [星链计划2.0](https://github.com/knownsec/404StarLink2.0-Galaxy)中的一环，如果对WhetherMysqlSham有任何疑问又或是想要找小伙伴交流，可以参考星链计划的加群方式。

- [https://github.com/knownsec/404StarLink2.0-Galaxy#community](https://github.com/knownsec/404StarLink2.0-Galaxy#community)

##
## 检测目标Mysql数据库是不是蜜罐,获取目标数据库详细信息
## [Release](https://github.com/BeichenDream/WhetherMysqlSham/raw/master/Release/WhetherMysqlSham.exe)

##   0x00
#### 获取到的信息并不完全准确,还要根据ThreadId进行比对
#### 在外网环境中,如果其它两项为false,peculiarity为true,请点击两次GetServerInfo,如果ThreadId自增1,通常这样的数据库不是蜜罐就是被遗忘的数据库
#### 在内网中,如果三项都为false,请换一个IP并尝试用正常的方式连接数据库,如果不提示密码错误,那这台机器大几率是蜜罐
##
####  0x01 获取目标数据库详细信息ThreadId还有ServerGreeting

![ServerInfo](https://raw.githubusercontent.com/BeichenDream/WhetherMysqlSham/master/png/ShamInfo.jpg)

####  0x02 判断目标是不是蜜罐

##### Sham

![Sham](https://raw.githubusercontent.com/BeichenDream/WhetherMysqlSham/master/png/Sham.jpg)

##### NoSham

![NoSham](https://raw.githubusercontent.com/BeichenDream/WhetherMysqlSham/master/png/NoSham.jpg)
