//-------------------------------------示例代码-------------------------------------

//初始化加载方法
//$(function () {
//})

//ajaxPost方法
//$.post('url',{"参数名":"参数值"}, function (result) {
//}); 

//打开只有内容窗口的
//window.open(url, "", "resizable=yes,status=yes,menubar=yes,scrollbars=yes,width=600,height=400", false);

//标签附加移除属性
// $("#test").attr("disabled", "disabled");
// $("#test").removeAttr("readonly")

//标签增加样式
//$("#test").css("border", "2px solid red");

//清空表单中内容
//function Reset() {
//    $("input[type=text]", document.forms['myForm']).each(function () {
//        if (this.id != "") {
//            $(this).textbox('setValue', '');//赋值 
//            //$(this).val();
//        }
//    });
//} 

//加载时,给所有input增加样式.并增加鼠标事件,在事件中修改样式 
//$("input[type='text']", $("#table_user")).addClass("myInput");
//$("input[type='text']", $("#table_user")).each(function () {
//    $(this).mouseover(function () {
//        //判断是否是easyui的控件,如果不是则添加事件以及样式
//        if (this.className.indexOf('text') == -1 && this.className.indexOf('easyui') == -1) {
//            //$(this).css({ height: "18px",  border: "1px solid #95b8e7" })
//            $(this).addClass("myInputMouseOver").removeClass("myInputMouseOut")
//        }
//    }).mouseout(function () {
//        if (this.className.indexOf('text') == -1 && this.className.indexOf('easyui') == -1) {
//            //$(this).css({ height: "20px", border: "0px" })
//            $(this).addClass("myInputMouseOut").removeClass("myInputMouseOver");
//        }
//    });
//});

//多个下拉框填充相同数据时,一次获取,一次赋值!
//var sfJSON;
//$(function () {
//    $.post('../Handler/GlobeHandler.ashx?action=GetSF', function (result) {
//        if (result == null || result == "") {
//            $.messager.show({
//                title: 'Error',
//                msg: "加载数据失败!"
//            });
//        }
//        else {
//            sfJSON = JSON.parse(result);
//        }
//    }); 
//    $('#a,#b,#c,#d,#e,#f,#g,#h').combobox('loadData', sfJSON);
//});
