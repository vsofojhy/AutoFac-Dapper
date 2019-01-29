# AutoFac-Dapper
基于AutoFac+Dapper下的简易WebApi框架
DB.Entity
    Entity.Base.ModelBase 主键ID约束
    Entity.Users 用户类
    IRepository.IUsersRepositoty 用户操作接口
    Repository.UsersRepository 用户操作类
DBAccess
    ConfigTools 数据库配置读取
    DbFactory 数据库对象工厂类（Mysql，SqlServer.....）
    IDatabase 获取数据库对象
    IRepositoryBase 数据库业务接口
    RepositoryBase 数据库业务实现
