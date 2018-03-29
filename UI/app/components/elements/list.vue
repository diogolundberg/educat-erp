<template>
  <div class="table bg-white shadow2 m2">
    <div class="table-row divider-bottom">
      <div
        v-for="column in columns"
        :key="column.name"
        class="table-cell px3 py1">
        {{ column.title }}
      </div>
    </div>
    <div
      v-for="(row, index) in value"
      :key="index"
      class="table-row divider-bottom">
      <div
        v-for="column in columns"
        :key="column.name"
        class="table-cell px3 py1">
        {{ row[column.name] }}
      </div>
    </div>
  </div>
</template>

<script>
  export default {
    name: "List",
    props: {
      value: {
        type: Array,
        required: true,
      },
      columns: {
        type: Array,
        required: true,
      },
    },
  };
</script>
