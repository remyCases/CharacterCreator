function scr_mod_cc_reset_skills()
{
    var _learned_skills = -1;

    with (o_skill_category)
    {
        if (id != o_skill_category_basic_skills.id)
        {
            var _size = array_length(skill)
            for (var i = 0; i < _size; i++)
            {
                with (skill[i])
                {
                    if (!is_enemy_skill)
                    {
                        if (is_open)
                        {
                            _learned_skills += 1;
                        }
                        is_open = 0
                    }

                }
            }
        }
    }

    var _len_skills_on_panel = ds_list_size(o_skill_fast_panel.skills)
    for (var _i = 0; i < _len_skills_on_panel; i++)
    {
        var _curr_skill = ds_list_find_value(o_skill_fast_panel.skills, _i) 
        if (_curr_skill != o_skill_trap_search)
        {
            ds_list_replace(o_skill_fast_panel.skills, _i, -4);
        }
    }

    scr_atr_incr("SP", _learned_skills);

    scr_classInit("tiers")
    scr_classInit("perks")
    with (o_skill_set_campfire_ico)
        is_open = 1

    with (o_skill_ico)
    {
        attributes_left = attributes_value_to_open
    }

    with (o_skillConnectionsRender)
        event_user(0)

    var STR = scr_atr("STR")
    var AGL = scr_atr("AGL")
    var PRC = scr_atr("PRC")
    var Vitality = scr_atr("Vitality")
    var WIL = scr_atr("WIL")

    var _ap_spent = STR - 10 + AGL - 10 + PRC - 10 + WIL - 10 + Vitality - 10;
    scr_atr_incr("AP", _ap_spent);

    scr_atr_set("STR", 10);
    scr_atr_set("AGL", 10);
    scr_atr_set("PRC", 10);
    scr_atr_set("Vitality", 10);
    scr_atr_set("WIL", 10);

    scr_atr_calc(o_player);
}