namespace BiliToolBox.Entitys.User
{
    public class Label
    {
        public string? path { get; set; }

        /// <summary>
        /// 会员类型文案
        /// </summary>
        public string? text { get; set; }

        /// <summary>
        /// 会员标签
        /// </summary>
        public string? label_theme { get; set; }

        /// <summary>
        /// 会员标签
        /// </summary>
        public string? text_color { get; set; }

        public int bg_style { get; set; }

        /// <summary>
        /// 会员标签背景颜色
        /// </summary>
        public string? bg_color { get; set; }

        /// <summary>
        /// 会员标签边框颜色	
        /// </summary>
        public string? border_color { get; set; }
    }

    public class Vip
    {
        /// <summary>
        /// 会员类型
        /// </summary>
        /// <remarks>
        /// 0：无
        /// 1：月大会员
        /// 2：年度及以上大会员
        /// </remarks>
        public int type { get; set; }
        /// <summary>
        /// 会员状态
        /// </summary>
        /// <remarks>
        /// 0：无
        /// 1：有
        /// </remarks>
        public int status { get; set; }
        /// <summary>
        /// 会员过期时间
        /// </summary>
        /// <remarks>Unix时间戳(毫秒)</remarks>
        public int due_date { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int vip_pay_type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int theme_type { get; set; }
        /// <summary>
        /// 会员标签
        /// </summary>
        public Label label { get; set; }
        /// <summary>
        /// 是否显示会员图标
        /// </summary>
        public int avatar_subscript { get; set; }
        /// <summary>
        /// 会员昵称颜色
        /// </summary>
        public string nickname_color { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int role { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string avatar_subscript_url { get; set; }
    }

    public class Pendant
    {
        /// <summary>
        /// 头像框id
        /// </summary>
        public int pid { get; set; }

        /// <summary>
        /// 头像框名称
        /// </summary>
        public string? name { get; set; }

        /// <summary>
        /// 头像框图片url
        /// </summary>
        public string? image { get; set; }

        public int expire { get; set; }

        public string? image_enhance { get; set; }

        public string? image_enhance_frame { get; set; }
    }

    public class Nameplate
    {
        /// <summary>
        /// 勋章id
        /// </summary>
        public int nid { get; set; }

        /// <summary>
        /// 勋章名称
        /// </summary>
        public string? name { get; set; }

        /// <summary>
        /// 挂件图片url 正常
        /// </summary>
        public string? image { get; set; }

        /// <summary>
        /// 勋章图片url 小
        /// </summary>
        public string? image_small { get; set; }

        /// <summary>
        /// 勋章等级
        /// </summary>
        public string? level { get; set; }

        /// <summary>
        /// 勋章条件
        /// </summary>
        public string? condition { get; set; }
    }

    public class Official
    {
        /// <summary>
        /// 认证类型
        /// </summary>
        public int role { get; set; }

        /// <summary>
        /// 认证信息
        /// </summary>
        public string? title { get; set; }

        /// <summary>
        /// 认证备注
        /// </summary>
        public string? desc { get; set; }

        /// <summary>
        /// 是否认证
        /// </summary>
        public int type { get; set; }
    }

    public class Profession
    {
        public int id { get; set; }

        public string? name { get; set; }

        public string? show_name { get; set; }
    }

    public class Level_exp
    {
        /// <summary>
        /// 当前等级
        /// </summary>
        public int current_level { get; set; }

        /// <summary>
        /// 指当前等级从多少经验值开始
        /// </summary>
        public int current_min { get; set; }

        /// <summary>
        /// 当前账户的经验值
        /// </summary>
        public int current_exp { get; set; }

        /// <summary>
        /// 下一个等级所需的经验值**(不是还需要多少)**
        /// </summary>
        public int next_exp { get; set; }
    }

    public class UserBaseInfo
    {
        public int mid { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string? name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string? sex { get; set; }

        /// <summary>
        /// 头像图片url
        /// </summary>
        public string? face { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        public string? sign { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int rank { get; set; }

        /// <summary>
        /// 等级
        /// </summary>
        public int level { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int jointime { get; set; }

        /// <summary>
        /// 节操
        /// </summary>
        public int moral { get; set; }

        /// <summary>
        /// 封禁状态
        /// </summary>
        public int silence { get; set; }

        /// <summary>
        /// 已验证邮箱
        /// </summary>
        public int email_status { get; set; }

        /// <summary>
        /// 已验证手机号
        /// </summary>
        public int tel_status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int identification { get; set; }

        /// <summary>
        /// 大会员状态
        /// </summary>
        public Vip? vip { get; set; }

        /// <summary>
        /// 挂件
        /// </summary>
        public Pendant? pendant { get; set; }
        /// <summary>
        /// 勋章
        /// </summary>
        public Nameplate? nameplate { get; set; }
        /// <summary>
        /// 认证信息
        /// </summary>
        public Official? official { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public int birthday { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int is_tourist { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int is_fake_account { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int pin_prompting { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int is_deleted { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int in_reg_audit { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? is_rip_user { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Profession? profession { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Level_exp? level_exp { get; set; }

        /// <summary>
        /// 硬币数
        /// </summary>
        public int coins { get; set; }

        /// <summary>
        /// 粉丝数
        /// </summary>
        public int following { get; set; }

        /// <summary>
        /// 粉丝数
        /// </summary>
        public int follower { get; set; }
    }
}