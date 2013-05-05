// JavaScript Document
// 树状菜单
var indx = 0;
var l2Indx = 0;
$(document).ready(function(){
	//一级展开
   $(".l1").toggle(function(){
	   
	 $(".slist").animate({height: 'toggle', opacity: 'hide'}, "slow");
     $(this).next(".slist").animate({height: 'toggle', opacity: 'toggle'}, "slow");
	 indx = $(this).index();
   },function(){
	   
	     $(".slist").animate({height: 'toggle', opacity: 'hide'}, "slow");
		 //判断是否是本节点，只有不是本节点才打开
		 if(indx != $(this).index()){
			$(this).next(".slist").animate({height: 'toggle', opacity: 'toggle'}, "slow");	 
			indx = $(this).index()
		 }
   });
   //二级
   $(".l2").toggle(function(){
	   $(".sslist").animate({height: 'toggle', opacity: 'hide'}, "slow");
     $(this).next(".sslist").animate({height: 'toggle', opacity: 'toggle'}, "slow");
	 l2Indx = $(this).index();
   },function(){
	   	 $(".sslist").animate({height: 'toggle', opacity: 'hide'}, "slow");
		 if(l2Indx != $(this).index()){
		 	$(this).next(".sslist").animate({height: 'toggle', opacity: 'toggle'}, "slow");
			
		 }
   });
   
 
   //全部关闭，展开的方法<h1 class="title"><span class="close">全部收起/展开</span>树形菜单</h1>
   /*
   $(".close").toggle(function(){
	$(".slist").animate({height: 'toggle', opacity: 'hide'}, "fast");  
	$(".sslist").animate({height: 'toggle', opacity: 'hide'}, "fast");  
	 },function(){
	$(".slist").animate({height: 'toggle', opacity: 'show'}, "fast");  
	$(".sslist").animate({height: 'toggle', opacity: 'show'}, "fast");  
	});  
	*/
});