/*
    * Auther:Mike.Jiang
    * Email: dataadapter@hotmail.com
    * Date: 2012-09-05
    */
    /*
    ��Ҫ˼�룺
    1>��ԭ�е�TABLE�е�THEADԪ�ظ���һ�ݷ���һ���µ�DIV(fixedheadwrap)��
    2>�������fixedheadwrapΪ����λ��ԭ����TABLE��THEADλ��
    */
    (function ($) {
        $.fn.extend({
            FixedHead: function (options) {
                var op = $.extend({ tableLayout: "auto" }, options);
                return this.each(function () {
                    var $this = $(this); //ָ��ǰ��table
                    var $thisParentDiv = $(this).parent(); //ָ��ǰtable�ĸ���DIV�����DIVҪ�Լ��ֶ�����ȥ
                    $thisParentDiv.wrap("<div class='fixedtablewrap'></div>").parent().css({ "position": "relative" }); //�ڵ�ǰtable�ĸ���DIV�ϣ��ټ�һ��DIV
                    var x = $thisParentDiv.position();

                    var fixedDiv = $("<div class='fixedheadwrap' style='clear:both;overflow:hidden;z-index:2;position:absolute;' ></div>")
                .insertBefore($thisParentDiv)//�ڵ�ǰtable�ĸ���DIV��ǰ���һ��DIV����DIV������װtabelr�ı�ͷ
                .css({ "width": $thisParentDiv[0].clientWidth, "left": x.left, "top": x.top }); 
                    var $thisClone = $this.clone(true);
                    $thisClone.find("tbody").remove(); //����һ��table������tbody�е�����ɾ���������ͽ���thead������Ҫ����ı�ͷҪ����thead��
                    $thisClone.appendTo(fixedDiv); //����ͷ��ӵ�fixedDiv��

                    $this.css({ "marginTop": 0, "table-layout": op.tableLayout });
                    //��ǰTABLE�ĸ���DIV��ˮƽ����������ˮƽ����ʱ��ͬʱ������װthead��DIV
                    $thisParentDiv.scroll(function () {
                        fixedDiv[0].scrollLeft = $(this)[0].scrollLeft;
                    });

                    //��Ϊ�̶���ı�ͷ��ԭ���ı����뿪�ˣ��������һЩ�������
                    //����Ĵ����ǽ�ԭ�������ÿһ��TD�Ŀ�ȸ����µĹ̶���ͷ
                    var $fixHeadTrs = $thisClone.find("#tb thead tr");
                    var $orginalHeadTrs = $this.find("#tb thead");
                    $fixHeadTrs.each(function (indexTr) {
                        var $curFixTds = $(this).find("th");
                        var $curOrgTr = $orginalHeadTrs.find("tr:eq(" + indexTr + ")");
                        $curFixTds.each(function (indexTd) {
                            $(this).css("width", $curOrgTr.find("th:eq(" + indexTd + ")").width());
                        });
                    });
                });
            }
        });
    })(jQuery);