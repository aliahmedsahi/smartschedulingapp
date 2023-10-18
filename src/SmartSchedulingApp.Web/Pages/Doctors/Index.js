$(function () {
    var l = abp.localization.getResource('SmartSchedulingApp');
    var scheduleModal = new abp.ModalManager(abp.appPath + 'Persons/ScheduleModal');
    var dataTable = $('#DoctorsTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            searching: false,
            scrollX: true,
            paging: false,
            sorting: false,
            ajax: abp.libs.datatables.createAjax(smartSchedulingApp.doctors.doctor.getList),
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
                    title: l('Specialization'),
                    data: "specialization"
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
                                        scheduleModal.open({ id: data.record.id, personType: 'Doctor' });
                                    }
                                },
                            ]
                    }
                }
            ]
        })
    );

    scheduleModal.onResult(function () {
        dataTable.ajax.reload();
    });
});
