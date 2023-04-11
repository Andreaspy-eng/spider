namespace spider.YandexApi.Result
{
    public class Options
    {
        public bool absolute_time { get; set; }
        public int average_projection_walking_part_distance_m { get; set; }
        public bool avoid_tolls { get; set; }
        public List<BalancedGroup> balanced_groups { get; set; }
        public bool close_locations_during_post_optimization_only { get; set; }
        public int critical_lateness_risk_probability { get; set; }
        public string date { get; set; }
        public int default_speed_km_h { get; set; }
        public bool enable_aggressive_time_adjustment { get; set; }
        public bool enable_attenuation_coefficient { get; set; }
        public bool enable_k_opt_move { get; set; }
        public bool enable_remove_vehicle { get; set; }
        public double eta_model_sigma { get; set; }
        public bool fix_planned_shifts { get; set; }
        public int global_proximity_factor { get; set; }
        public bool ignore_min_stops_for_unused { get; set; }
        public bool ignore_zones { get; set; }
        public List<List<string>> incompatible_load_types { get; set; }
        public bool load_when_ready { get; set; }
        public int max_drop_penalty_percentage { get; set; }
        public bool merge_multiorders { get; set; }
        public string minimize { get; set; }
        public bool minimize_lateness_risk { get; set; }
        public bool penalize_late_service { get; set; }
        public Penalty penalty { get; set; }
        public bool post_optimization { get; set; }
        public string probability_metric { get; set; }
        public int proximity_factor { get; set; }
        public int rand_seed { get; set; }
        public bool restart_on_drop { get; set; }
        public string routing_mode { get; set; }
        public int solver_time_limit_s { get; set; }
        public int temperature { get; set; }
        public int thread_count { get; set; }
        public int time_zone { get; set; }
        public double vehicle_remove_gradient_coefficient { get; set; }
        public bool wait_in_multiorders { get; set; }
        public bool walking_on_postoptimization_only { get; set; }
        public bool weighted_drop_penalty { get; set; }
    }
}