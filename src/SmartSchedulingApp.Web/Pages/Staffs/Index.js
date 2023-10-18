$(function () {
    var l = abp.localization.getResource('SmartSchedulingApp');
    var scheduleModal = new abp.ModalManager(abp.appPath + 'Persons/ScheduleModal');
    var dataTable = $('#StaffTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            searching: false,
            scrollX: true,
            paging: false,
            sorting: false,
            ajax: abp.libs.datatables.createAjax(smartSchedulingApp.staffs.staff.getList),
            columnDefs: [
                {
                    title: l('FirstName'),
                    data: "user.name"
                },
                {
                    title: l('LastName'),
                    data: "user.surname"
                },
                {
                    title: l('Email'),
                    data: "user.email"
                },
                {
                    title: l('Age'),
                    data: "age"
                },
                {
                    title: l('Gender'),
                    data: "gender"
                },
                {
                    title: l('Department'),
                    data: "department"
                },
                {
                    title: l('Actions'),
                    outerWidth: 50,
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('AddSchedule'),
                                    action: function (data) {
                                        scheduleModal.open({ id: data.record.id, personType: 'Staff' });
                                    }
                                },
                            ]
                    }
                }
            ]
        })
    );

});
