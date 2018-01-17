using System.Collections.Generic;

namespace DMSApi.Models.IRepository
{
    public interface IControlRepository
    {
        object GetAllControlsOnly();

        object GetAllControls();

        List<control> GetAllControlForPermission();

        List<control> GetControlById(long control_id);

        control GetControlByName(string control_name);

        control GetControlByParentId(long control_parent_id);

        control GetControlByTypeId(long control_type_id);

        control GetControlBySort(long control_sort);

        control GetControlByAlias(string control_alias);

        control GetControlByController(string control_controller);

        control GetControlByAction(string control_action);

        bool CheckControlForDuplicateByName(string control_name);

        bool InsertControl(control ocontrol);

        bool UpdateControl(control ocontrol);

        bool DeleteControl(long control_id);
    }
}