export const trackedCourseFrameworkToCourseFramework = (tracked: TrackedCourseFramework): CourseFramework => {
  return {
    code: tracked.code.value,
    name: tracked.name.value,
    semester: tracked.semester.value,
    theoryHours: tracked.weighting.theoryHours.value,
    laboratoryHours: tracked.weighting.laboratoryHours.value,
    personalWorkHours: tracked.weighting.personnalWorkHours.value,
    competencies: tracked.competencies,
  };
};
