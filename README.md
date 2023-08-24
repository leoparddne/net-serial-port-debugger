# NetSerialPortDebugger

#### 介绍
串口调试工具，使用网络转发串口数据以实现调试远程串口的需求

#### 软件架构
程序主体使用.net6实现  
基础结构划分情况如下  
SerialPortProxyService.Common - 核心、公共逻辑
SerialPortProxyService.Console - 命令行模式逻辑
SerialPortProxyService.Win - GUI模式逻辑，基于WPF+handycontrol
SerialPortProxyService - 服务模式逻辑
SerialPortProxyService.Test - 测试代码,用作部分单元测试不便于测试的场景
SerialPortProxyService.MSTest - 单元测试,基于MSTest框架

#### 安装教程

.net6环境

#### 使用说明
提供双向通信功能，
将串口数据通过网络转发，
网口获取到的数据再发送给串口


其他  
互联网环境中需要进行内网穿透的配置，或是使用局域网中的路由器所提供的NAT功能临时映射到本地ip


### 程序运行模式
提供如下几种运行模式
1. GUI模式 - 作为工具性质场景使用
2. 命令行模式 - 此模式可以作为中间件在其他需要使用的位置进行调用 - 此模式支持跨平台场景
3. 系统服务模式 - 此模式可以部署为系统服务,支持windows及linux下systemd - 次模式支持跨平台场景


### 最佳实践
此处将具体实际串口设备的一端称为客户端, 将对端称呼为服务端。  
客户端需要配置服务端的ip、端口以及监听的串口。  
服务端监听本机所有的ip(当前默认端口为5000)、需要监听的串口。  


服务端需要通过虚拟串口工具模拟一对串口，
本程序监听其中一个串口，数据将自动转发到配对的串口中。 
客户端需要添加一个虚拟串口将其与原有的串口绑定。
需要注意首先需要启动服务端,随后启动客户端。  
后续所有串口消息均会通过网络进行转发，此过程是双向转发。  



#### 参与贡献

1.  Fork 本仓库
2.  新建 Feat_xxx 分支
3.  提交代码
4.  新建 Pull Request


#### 特技

1.  使用 Readme\_XXX.md 来支持不同的语言，例如 Readme\_en.md, Readme\_zh.md
2.  Gitee 官方博客 [blog.gitee.com](https://blog.gitee.com)
3.  你可以 [https://gitee.com/explore](https://gitee.com/explore) 这个地址来了解 Gitee 上的优秀开源项目
4.  [GVP](https://gitee.com/gvp) 全称是 Gitee 最有价值开源项目，是综合评定出的优秀开源项目
5.  Gitee 官方提供的使用手册 [https://gitee.com/help](https://gitee.com/help)
6.  Gitee 封面人物是一档用来展示 Gitee 会员风采的栏目 [https://gitee.com/gitee-stars/](https://gitee.com/gitee-stars/)
