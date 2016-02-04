$(function(){

    //Uncomment the line and switch modes
    //$.fn.editable.defaults.mode = 'inline';
    
    //editables 
    $('#username').editable({
     url: '/post',
     type: 'text',
     pk: 1,
     name: 'username',
     title: 'Enter username'
 });
    
    $('#firstname').editable({
        validate: function(value) {
         if($.trim(value) == '') return 'This field is required';
     }
 });
    
    $('#sex').editable({
        prepend: "not selected",
        source: [
        {value: 1, text: 'Male'},
        {value: 2, text: 'Female'}
        ],
        display: function(value, sourceData) {
           var colors = {"": "gray", 1: "green", 2: "blue"},
           elem = $.grep(sourceData, function(o){return o.value == value;});

           if(elem.length) {
               $(this).text(elem[0].text).css("color", colors[value]);
           } else {
               $(this).empty();
           }
       }
   });
    
    $('#status').editable();
    
    $('#group').editable({
        showbuttons: false
    });

    $('#dob').editable();

    $('#comments').editable({
        showbuttons: 'bottom'
    });

});