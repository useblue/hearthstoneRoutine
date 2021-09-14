# 炉石兄弟个人策略分享

**声明： 使用炉石兄弟辅助进行游戏的行为违反了暴雪[用户协议](https://www.battlenet.com.cn/zh/legal-cn/eula) 1.C.ii.2 使用机器人程序软件（BOT）： 任何未经暴雪和/或运营方明确授权，允许自动控制游戏，服务器和/或任何组件或其功能的的代码或/和软件，如自动操控游戏中的角色；因此根据协议，暴雪和/或运营方可以暂停、撤销或终止您使用本平台或本平台的部分功能或组成部分的许可。**

#### 简介
这是基于HB公司的炉石兄弟软件改进的炉石机器人策略，不包含主程序，建立本项目完全出于个人对 ai 对战游戏的兴趣。

项目仅供学习交流使用，不会在任何场合贩卖脚本，之后会删除之前的主程序分享并不再主动进行主程序的分享。如果该策略的开源分享行为损害了暴雪/运营方的利益，请联系我停止更新/删库，我会全力配合。

#### 伏笔反馈说明
伏笔反馈请提交 issue

提交时请将需要重新计算的场面（通常是回合刚开始的场面也就是当回合第 0 步）粘贴到 ### 对局记录文件 处

反馈的场面对我优化策略很有帮助，提交反馈时请务必添加场面日志文件。

**主程序带来的任何问题请不要问我，不会回答。**

#### 快速部署
1. 打开 Visual Studio 新建项目

2. 选择从已有项目中新建->从现有文件新建项目，类型为 Visual C#，选中 Routines/DefaultRoutine 目录。 

3. 右键项目，点击添加->引用，在程序集中勾选 Windows Base、PresentationCore、presentationFramework、Xaml，浏览目录引入兄弟主程序（Hearthbuddy.exe)，如果添加引用后报二义性错误可能主程序有内置的策略，与我的项目产生了冲突，请尝试更换一个主程序引入）、兄弟目录下所有 .dll 文件依赖（如有报错请尝试一一排除）

4. 右键项目->属性->启动对象设置为 AutoJudge 并保存

4. 推荐选择目标框架为 4.7 

5. 修改 Routines\DefaultRoutine\Silverfish\Test\AutoJudge.cs 和 TestBase.cs 文件中包含的测试文件夹路径和需要测试的确保正确

5. 点击 debug 运行对所有测试用例的自动化测试

6. 自动测试程序会在文件夹下对于每个 .txt 对局记录文件会生成一个 .result 计算得出的打法方案，和 .detail 计算过程文件，同时比较 .result 文件和 .answer 文件中最终场面是否相同。在确保兄弟计算得出的打法正确后，请将 .result 文件后缀改为 .answer，以便下一次测试时自动对这个测试用例进行校验。对局记录文件会在兄弟运行时自动生成，位置在 Silverfish\Routines\DefaultRoutine\Silverfish\Test\Data\对局记录 中。

#### 目录说明
- DefaultRoutine 自定义策略目录
    - Silverfish 自定义策略
        - ai 核心逻辑
            - action.cs 每个模拟行动以一个 Action 为单位
            - ActionNormalizer.cs 对 Action 列表进行排序（目前基本不使用这种方式）
            - **Ai.cs** 定义了场面计算相关的各个参数和找到最优场面的主函数
            - **Behavior.cs** 自定义行为模式的虚拟类
            - BoardTester.cs 测试类，输入给定的场面，输出计算得出的最优行动
            - **CardDB.cs** 定义卡牌
            - cardIDEnum.cs 以枚举形式定义每张卡的 id
            - cardName.cs 以枚举形式定义每张卡的 name
            - ComboBreaker.cs 连招，目前基本不使用这种方式
            - Debug.cs 断点调试可以在这里设置想要调试的层数和编号（基本废弃）
            - DeckManager.cs 维护牌库信息
            - EnemyTurnSimulator.cs 模拟对手回合（基本废弃）
            - Handmanager.cs 手牌管理
            - Helpfunctions.cs 打印类，用于打印自定义信息
            - **Hrtprozis.cs** 定义一场对局的状态
            - **Minion.cs** 定义随从
            - **MiniSimulator.cs** 深搜找到最优场面
            - MiniSimulatorNextTurn.cs 计算下回合场面
            - Movegenerator.cs 判断行动合法性
            - Mulligan.cs 控制留牌逻辑
            - PenalityManager.cs 惩罚类，目前基本选择在自定义行为模式中单独定义
            - PenTemplate.cs 惩罚虚拟类
            - **Playfield.cs** 定义一个场面的所有属性
            - Probalilitymaker.cs 记录奥秘、对局中出的牌
            - Questmanager.cs 主线任务、任务线和支线任务
            - RulesEngine.cs 加载自定义行为模式中定义的规则(目前基本不使用该方法)
            - Setting.cs 加载自定义行为模式中定义的设置
            - **SimTemplate.cs** 卡牌效果函数模板
            - TAGGS.cs 各种枚举
            - Utils.cs 计算工具类
            - Weapon.cs 武器
            - 打印工具.cs 记录一些自定义的状态信息
        - behavior 单套卡组的自定义行为模式
            - \_combo.txt 连击（废弃）
            - \_mulligan.txt 留牌策略
            - \_rules.txt 规则 (废弃)
            - \_settings.txt 配置信息
            - **Behavior###.cs / Penality###.cs** 自定义的卡组行为模式，场面的价值判断依据
        - cards 单张卡牌的模拟效果
        - data 所有卡牌的定义
            - CardDefs.xml 定义了每张卡牌的牌面参数
        - Helpers 工具类
            - CardHelper.cs 根据 xml 文件和卡牌 sim 初始化对每张卡牌生成一个卡牌对象
        - Test 测试目录
            - Data 测试数据
                - 对局记录 运行中生成的对局记录文件
                    - 日期yyyy-MM-dd 当日的对局记录
                        - hh-mm-ss-own-enemy 单场对局记录
            - AiTest.cs 测试类
            - AutoJudge.cs 对一个文件夹下所有测试用例，和答案进行比较判断是否通过测试
            - TestBase.cs 记录测试目录位置基本信息
        - UltimateLogs 日志，暂无作用
            - Logg.txt 运行中生成的日志文件
    - DefaultRoutine.cs 定义兄弟与炉石进行交互的逻辑
    - DefaultRoutineSetting.cs 绑定界面交互元素
    - SettingsGui.xaml 自定义策略的界面
    - silverfish_HB.cs 在运行过程记录和更新场面状态，打印对局记录和日志

#### 场面计算逻辑简单说明
1. 首先每个策略对于每个场面（包括敌我生命值、武器、随从、手牌、奥秘等所有能获取的信息）都可以根据 getPlayfieldValue 函数计算得到一个场面值
2. 关于如何出牌，程序会通过宽度优先搜索获取一颗树，树上的每个节点都包括一个 Action 列表和最终的场面；程序会选择所有树节点中得分最高的节点的 Action 列表执行操作

##### 关于策略的场面值主要设置
1. 根据策略风格不同，重写生命值价值计算函数 getHpValue
2. 根据策略风格不同，重写随从价值判断函数 getMyMinionValue、 getEnemyMinionValue ，主要控制随从交换逻辑，如防战策略中我方随从（白板）价值等于（生命值 + 攻击力） * 3 + 5 ，而敌方随从价值（白板）等于（生命值 + 攻击力） * 4 + 10，AI 就会更加积极进行随从交换，哪怕但从身材上来说交换是亏的；而骑士策略中敌我白板的价值均为（生命值 + 攻击力）* 4,因此骑士策略在随从交换无法取得较高收益的情况下会倾向于打脸
3. 修正随从价值： 程序无法准确判断随从异能的价值，因此可以在 getMyMinionValue、 getEnemyMinionValue 函数中对于特定随从或者异能修正随从价值
4. 设置出牌惩罚： 在 getComboPenality 函数中对于不同的牌设置惩罚以修正程序打法。比如设置打出斩杀的惩罚为 29 点，程序就会认为出牌解掉对手的 3/2 白板（价值为 ( 3 + 2 ) * 4 + 10 = 30 ）才勉强值得出牌解决
5. 调整场面值和惩罚以通过各种测试用例，优化策略
 
#### 参考资料
- 兄弟目录介绍，非常详细：http://blog.wjhwjhn.com/archives/16/
- 炉石传说卡牌数据库：https://hs.fbigame.com/
- 留牌策略转文字：http://ls.varc.cn/

**该项目建立的目的仅仅是为了满足个人对策略研究的兴趣，个人精力有限需要同好帮我找到各种意想不到的伏笔修正以优化兄弟的打法。根据个人主观意愿随时可能做出停更乃至删库跑路的决定，还望见谅。**