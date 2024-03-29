import jsonp from "jsonp";

exports.parseDate = function parseDate(value) {
  if (!value || value.length !== 10) {
    return null;
  }
  const [day, month, year] = value.split(/\D+/);
  if (!(day && month && year && year > 0)) {
    return null;
  }
  return new Date(year, month - 1, day);
};

exports.yearsAgo = function yearsAgo(value) {
  if (!value) {
    return 0;
  }
  const diff = new Date() - value;
  const year = 1000 * 60 * 60 * 24 * 365;
  return Math.floor(diff / year);
};

exports.daysAgo = function daysAgo(value) {
  if (!value) {
    return 0;
  }
  const diff = new Date() - value;
  const year = 1000 * 60 * 60 * 24;
  return Math.floor(diff / year);
};

exports.findZip = function findZip(value) {
  return new Promise((respond, error) => {
    const url = `http://api.postmon.com.br/v1/cep/${value}`;
    jsonp(url, (err, data) => {
      if (err) {
        error(err.message);
      } else {
        respond(data);
      }
    });
  });
};
