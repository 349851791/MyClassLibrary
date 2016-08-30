 using System;
 namespace Models
{
  /// <summary>
/// 用户表
/// </summary>
  public  class 有效申请登记
{
  /// <summary>
/// 
/// </summary>
[Column(Identity =true)]
  public   int? FLOWSN { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string CK_GUID { get; set; }

  /// <summary>
/// 
/// </summary>
  public   int? 申请序号 { get; set; }

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
  public   string 申请人 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 电话 { get; set; }

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
  public   string 矿山名称 { get; set; }

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
  public   int? 经济类型 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 审批机关 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 批准文号 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 投资额 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 投资额单位 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 注册资金 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 注册资金单位 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 资金来源 { get; set; }

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
  public   string 设计年限 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   int? 开采主矿种 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 其它主矿种 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 共伴生类型 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 设计规模 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 开采方式 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 采矿方法 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 选矿方法 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 采矿回采率 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 矿石贫化率 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 选矿回收率 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   int? 应缴纳采矿权价款 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 采深上限 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 采深下限 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 矿区面积 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 采矿权使用费 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 法定代表人 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 填表人 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   DateTime? 受理日期 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 有效期限 { get; set; }

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
  public   string 审查人 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 签发 { get; set; }

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
  public   DateTime? 签发时间 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 变更类型 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 变更内容 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   int? 矿区编码 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 采矿权取得方式 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   int? 取得方式代码 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 价款处置方式 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   int? 价款处置方式代码 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 勘查许可证号 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 总储量 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 储量单位 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 矿石类型 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 划矿备案号 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 原许可证号 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   DateTime? 原签发时间 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   DateTime? 原有效期起 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   DateTime? 原有效期止 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   DateTime? 填表时间 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 许可证副本说明 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 发证机关编码 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 发证机关名称 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   int? 所在行政区 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 所在行政区名称 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 审查人意见 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 最终产品 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 探矿权取得方式 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 价款处置方式说明 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 探明地质储量 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 设计利用储量 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 地质报告审批情况 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 矿石品位 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 综合回收 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 申请原因 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 补偿费交纳情况 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 使用费交纳情况 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 价款处置情况 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 合并项目 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 备注 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 项目类型名称 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 规划规模单位 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 矿山类型 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 关联转让审批文号 { get; set; }

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
/// 历史数据版本
/// </summary>
  public   string 历史数据版本 { get; set; }

  /// <summary>
/// 建设规模
/// </summary>
  public   string 建设规模 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 区域坐标 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 更新标识 { get; set; }

  /// <summary>
/// 
/// </summary>
  public   string 企业组织机构代码 { get; set; }

 }
}

