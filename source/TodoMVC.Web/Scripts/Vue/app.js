window.onload = function () {
  
    var app = new Vue({
        el: "#app",
        data: {
            list: []
            ,
            addItem:
            {
                status: false          
            },
            
     
     
        },
        created: function () {

            this.todoList();
        },
      
        methods: {
            todoList: function (status) {
                var me = this;
                $.ajax({
                    url: '/Vue/List',
                    type: 'get',
                    data: { status: status },
                    success: function (data) {
                        data = JSON.parse(data);                   
                        me.list = data;                
                        
                    }
                });
            },


            create: function (inputValue) {
                var me = this;
                $.ajax({
                    url: '/Vue/Create',
                    data: { content: this.inputValue },
                    type: 'post',
                    dataType: 'json',
                    success: function (data) {
                        data = JSON.parse(data);
                        me.list = data;
                        me.todoList();
                    }

                });

                inputValue = '';

            },


            clear: function () {
                var me = this;
                $.ajax({
                    url: '/Vue/Clear',
                    type: 'post',
                    success: function () {
                        me.todoList();
                    }
                });
            },

            delete1: function (deleteId, status) {
                var me = this;
                $.ajax({
                    url: '/Vue/Delete',
                    data: { id: deleteId },
                    type: 'post',
                    dataType: 'json',
                    success: function (data) {
                        me.todoList(status);
                    }
                });
            },

            changeStatus: function (changeId, status) {
                var me = this;
                $.ajax({
                    url: '/Vue/Update',
                    data: { id: changeId },
                    type: 'post',
                    dataType: 'json',
                    success: function (data) {
                        me.todoList(status);
                    }
                });
            },
           


        }


    })
}
