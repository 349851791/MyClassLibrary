
//页面加载事件
function init() {
    //var myHeight = document.documentElement.clientHeight;
    //var myWidth = document.documentElement.clientWidth;
    //var topDivHeight = document.getElementById("topDiv").clientHeight;
    //var tempHeight = myHeight - topDivHeight - 26;
    //document.getElementById("accordionMenu").style.height = tempHeight + "px";

    //var layoutMenusHeight = document.getElementById("layoutMenus").style.height;
    //document.getElementById("accordionMenu").style.height = layoutMenusHeight.substr(0,layoutMenusHeight.length-2)-2 + "px";
    //document.getElementById("rightDiv").style.height = temp + "px";
    // document.getElementById("rightIframe").style.width = myWidth - 203 + "px";
    // document.getElementById("rightIframe").style.height = temp + "px"; 
    // document.getElementById("leftIframe").style.height = temp + "px"; 
}


//region 左侧目录树方法
//获取左侧数据
$(function () { 
    var w = document.body.clientWidth;
    var h = document.body.clientHeight;
    var left = "0px";
    var top = "0px";
    if (w > 400) {
        left = (w - 400) / 2 + "px";
    } if (h > 400) {
        top = (h - 300) / 2 + "px";
    }
    $("#loading_img").css("left", left);
    $("#loading_img").css("top", top);


    $.post('Handler/GlobalHandler.ashx?action=GetMenuForAccordion', function (result) {
        var jsonResult = JSON.parse(result);//eval('(' + result + ')'); 
        for (var i = 0; i < jsonResult.length; i++) {
            $('#accordionMenu').accordion('add', {
                title: jsonResult[i].NAME,
                iconCls: jsonResult[i].ICON,
                selected:false, //jsonResult[i].SELECTED,
                content:
                        function () {
                            var contents = "<table id='leftTable' class='LeftContent' border='0' cellpadding='0' cellspacing='0'>";
                            for (var j = 0; j < jsonResult[i].ChildrenList.length; j++) {
                                contents += "<tr><td onclick=\"showContent('" + jsonResult[i].ChildrenList[j].URL + "','" + jsonResult[i].ChildrenList[j].NAME + "')\">" + jsonResult[i].ChildrenList[j].NAME + "</td></tr>";
                               //contents += "<div style=\"cursor:pointer;margin-left:10px;margin-top:5px;\" onclick=\"showContent('" + jsonResult[i].ChildrenList[j].URL + "','" + jsonResult[i].ChildrenList[j].NAME + "')\">" + jsonResult[i].ChildrenList[j].NAME + "</div>";
                            }
                            contents += "</table>";
                            return contents;
                        }
            });
        }
        $("#Loading").fadeOut("slow", function () {
            $(this).remove();
        });
    });
})

//显示左侧内容
function showContent(url,name)
{
    document.getElementById("contentRight").src = url;
    $('#layoutContnet').panel({ title: name }); 
}
//endregion
