export const toDateOnly = (date: Date) => {
  if (!isDate(date)) {
    throw new Error('Invalid date provided to toDateOnly');
  }
  const year: number = date.getFullYear();
  const month = String(date.getMonth() + 1).padStart(2, '0'); // months are 0-indexed
  const day = String(date.getDate()).padStart(2, '0');

  return `${year}-${month}-${day}`;
};
