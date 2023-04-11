
namespace spider.YandexApi.Result
{

    public class Metrics2
    {
        public int arrival_after_start_penalty { get; set; }
        public int assigned_locations_count { get; set; }
        public int balanced_group_duration_deviation_s { get; set; }
        public int balanced_group_penalty { get; set; }
        public int balanced_group_stop_count_deviation { get; set; }
        public int depot_throughput_violation_kg { get; set; }
        public int depot_throughput_violation_kg_per_hour { get; set; }
        public int depot_throughput_violation_units { get; set; }
        public int depot_throughput_violation_units_per_hour { get; set; }
        public int depot_throughput_violation_vehicles { get; set; }
        public int drop_penalty_percentage { get; set; }
        public int dropped_locations_count { get; set; }
        public int early_depot_count { get; set; }
        public int early_locations_count { get; set; }
        public int early_shifts_count { get; set; }
        public int failed_dropped_breaks_count { get; set; }
        public int failed_dropped_breaks_duration_s { get; set; }
        public int failed_dropped_breaks_penalty { get; set; }
        public int failed_max_work_duration_count { get; set; }
        public int failed_min_work_duration_count { get; set; }
        public int failed_time_window_depot_count { get; set; }
        public int failed_time_window_depot_count_penalty { get; set; }
        public double failed_time_window_depot_duration_penalty { get; set; }
        public int failed_time_window_depot_duration_s { get; set; }
        public double failed_time_window_depots_total_penalty { get; set; }
        public int failed_time_window_locations_count { get; set; }
        public int failed_time_window_locations_count_penalty { get; set; }
        public double failed_time_window_locations_duration_penalty { get; set; }
        public int failed_time_window_locations_duration_s { get; set; }
        public double failed_time_window_locations_total_penalty { get; set; }
        public int failed_time_window_shifts_count { get; set; }
        public int failed_time_window_shifts_count_penalty { get; set; }
        public double failed_time_window_shifts_duration_penalty { get; set; }
        public int failed_time_window_shifts_duration_s { get; set; }
        public double failed_time_window_shifts_total_penalty { get; set; }
        public int failed_work_duration_count { get; set; }
        public int failed_work_duration_count_penalty { get; set; }
        public int failed_work_duration_penalty { get; set; }
        public int failed_work_duration_s { get; set; }
        public int failed_work_duration_total_penalty { get; set; }
        public double global_proximity { get; set; }
        public int late_depot_count { get; set; }
        public int late_locations_count { get; set; }
        public int late_shifts_count { get; set; }
        public int lateness_risk_locations_count { get; set; }
        public int max_distance_from_depot_m { get; set; }
        public int max_distance_to_attraction_point_m { get; set; }
        public int max_distance_to_garage_m { get; set; }
        public int max_drop_percentage_penalty { get; set; }
        public int max_vehicle_runs { get; set; }
        public int multiorders_extra_points { get; set; }
        public int multiorders_extra_vehicles { get; set; }
        public int multiorders_extra_visits { get; set; }
        public int number_of_routes { get; set; }
        public double objective_minimum { get; set; }
        public double operations_per_second { get; set; }
        public int optimization_steps { get; set; }
        public double overtime_duration_penalty { get; set; }
        public int overtime_duration_s { get; set; }
        public double overtime_penalty { get; set; }
        public int overtime_shifts_count { get; set; }
        public int overtime_shifts_count_penalty { get; set; }
        public double proximity { get; set; }
        public int total_close_location_distance_excess { get; set; }
        public int total_close_location_distance_penalty { get; set; }
        public int total_close_location_duration_excess { get; set; }
        public int total_close_location_duration_penalty { get; set; }
        public double total_cost { get; set; }
        public double total_cost_with_penalty { get; set; }
        public int total_custom_cost { get; set; }
        public int total_driving_projection_walking_penalty { get; set; }
        public int total_drop_penalty { get; set; }
        public double total_duration_cost { get; set; }
        public int total_duration_s { get; set; }
        public int total_early_count { get; set; }
        public int total_early_duration_s { get; set; }
        public double total_early_penalty { get; set; }
        public int total_failed_delivery_deadline_count { get; set; }
        public int total_failed_delivery_deadline_duration_s { get; set; }
        public int total_failed_delivery_deadline_penalty { get; set; }
        public int total_failed_time_window_count { get; set; }
        public int total_failed_time_window_duration_s { get; set; }
        public double total_failed_time_window_penalty { get; set; }
        public double total_fails_penalty { get; set; }
        public int total_fixed_cost { get; set; }
        public int total_global_proximity_distance_m { get; set; }
        public int total_global_proximity_duration_s { get; set; }
        public int total_global_proximity_penalty { get; set; }
        public double total_guaranteed_penalty { get; set; }
        public int total_late_count { get; set; }
        public int total_late_duration_s { get; set; }
        public double total_late_penalty { get; set; }
        public double total_lateness_risk_probability { get; set; }
        public int total_locations_cost { get; set; }
        public int total_mileage_penalty { get; set; }
        public int total_min_stop_weight_penalty { get; set; }
        public int total_multiorders_penalty { get; set; }
        public int total_optional_tags_cost { get; set; }
        public int total_parking_duration_excess { get; set; }
        public int total_parking_duration_penalty { get; set; }
        public int total_parking_walking_distance_excess { get; set; }
        public int total_parking_walking_distance_penalty { get; set; }
        public double total_penalty { get; set; }
        public double total_probable_penalty { get; set; }
        public double total_proximity_distance_m { get; set; }
        public double total_proximity_duration_s { get; set; }
        public int total_proximity_penalty { get; set; }
        public int total_rest_duration_s { get; set; }
        public int total_runs_cost { get; set; }
        public int total_service_duration_s { get; set; }
        public int total_stop_count_penalty { get; set; }
        public int total_stops { get; set; }
        public int total_trailer_rolling_cost { get; set; }
        public int total_trailer_rolling_count { get; set; }
        public int total_trailer_transit_distance_m { get; set; }
        public int total_trailer_transit_duration_s { get; set; }
        public double total_transit_distance_cost { get; set; }
        public int total_transit_distance_m { get; set; }
        public int total_transit_duration_s { get; set; }
        public int total_transport_work_cost { get; set; }
        public double total_transport_work_tonne_km { get; set; }
        public int total_unfeasibility_count { get; set; }
        public int total_unfeasibility_penalty { get; set; }
        public int total_unique_stops { get; set; }
        public int total_waiting_duration_s { get; set; }
        public int total_walking_courier_restrictions_penalty { get; set; }
        public int total_walking_distance_m { get; set; }
        public int total_walking_duration_s { get; set; }
        public int total_walking_edge_distance_excess { get; set; }
        public int total_walking_edge_penalty { get; set; }
        public int total_work_breaks { get; set; }
        public int transit_time_penalty { get; set; }
        public int used_vehicles { get; set; }
        public List<TasksSummary> _tasks_summary { get; set; }
        public int total_units { get; set; }
        public int total_volume_m3 { get; set; }
        public double total_weight_kg { get; set; }
        public int utilization_units { get; set; }
        public int utilization_units_perc { get; set; }
        public int utilization_volume_m3 { get; set; }
        public int utilization_volume_perc { get; set; }
        public double utilization_weight_kg { get; set; }
        public double utilization_weight_perc { get; set; }
    }
}