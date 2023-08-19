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
监听模式  
串口模式：监听本机串口并发送给指定的网络ip、端口
网络模式：监听本机网络将接收到的数据包转发给本机串口


其他  
互联网环境中需要进行内网穿透的配置，或是使用局域网中的路由器所提供的NAT功能临时映射到本地ip


### 程序运行模式
提供如下几种运行模式
1. GUI模式 - 作为工具性质场景使用
2. 命令行模式 - 此模式可以作为中间件在其他需要使用的位置进行调用 - 此模式支持跨平台场景
3. 系统服务模式 - 此模式可以部署为系统服务,支持windows及linux下systemd - 次模式支持跨平台场景



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
