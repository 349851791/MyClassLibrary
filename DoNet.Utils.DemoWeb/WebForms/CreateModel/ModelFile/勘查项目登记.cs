 using System;
 namespace Models
{
  /// <summary>
/// 用户表
/// </summary>
  public  class 勘查项目登记
{
  /// <summary>
/// 
/// </summary>
[Column(Identity =true)]
  public   string 申请序号 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 许可证号 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 项目档案号 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   int? 项目类型 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 项目名称 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   DateTime? 受理日期 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   DateTime? 发证日期 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   DateTime? 填表时间 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 申请人 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 勘查单位 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 资格证号 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 单位地址 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 法人代表 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 地址 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 邮编 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 电话 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 联络员 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 开户银行 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 帐号 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   int? 经济类型 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   int? 勘查矿种 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   int? 项目性质 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   int? 勘查阶段 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 地理位置 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   DateTime? 申请有效期起 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   DateTime? 申请有效期止 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   DateTime? 签发时间 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   DateTime? 有效期起 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   DateTime? 有效期止 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   DateTime? 矿权终止时间 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   DateTime? 首次设立时间 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 总面积 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 东经起 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 东经止 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 北纬起 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 北纬止 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   int? 基本区块数 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   int? 四分之一区块数 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 小区块数 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 折成基本区块 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 价款处置方式 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   int? 探矿权取得方式 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 金额 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 第一勘查年度 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 第二勘查年度 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 第三勘查年度 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 国家投资 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 地方投资 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 企业投资 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 外商投资 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 个人投资 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 其他投资 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 所在行政区 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 原许可证号 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 原项目名称 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 原申请序号 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 审查人 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   DateTime? 审查日期 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 复查 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 复核 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 审核 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 签发 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 会审意见1 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 会审意见2 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 会审意见3 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 会审意见4 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 会审意见5 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 累计缴纳探矿权使用费 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   int? 注销类型 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 探矿权价款处置情况 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 申请注销理由 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   int? 变更类型 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 变更原因 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 区域坐标 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 审查人意见 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 工作任务目的 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 工作人员配备 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 工作量 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 备注 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 附件 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 完成情况 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 申请理由 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 登记机关意见 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 注销备注 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 注销附件 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string FLOWSN { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 项目简介 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   int? 坐标系统 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   int? 实地核查状况 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 实地核查单位 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 实地核查人 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   DateTime? 实地核查完成时间 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 重叠核查单位 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 重叠核查人 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   DateTime? 重叠核查完成时间 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   int? 词典标识1 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   int? 词典标识2 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 数值标识1 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 数值标识2 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 文本标识1 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 文本标识2 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 实地核查坐标 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 实地核查意见 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 重叠核查意见 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 项目标签 { get; set; }

  /// <summary>
/// 历史数据版本
/// </summary>
  public   string 历史数据版本 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 企业组织机构代码 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 发证机关名称 { get; set; }

  /// <summary>
/// 单位性质
/// </summary>
  public   int? 单位性质 { get; set; }

 }
}

