<template>
  <div class="m2 flex flex-column items-center">
    <div class="table bg-white shadow2 m2">
      <div class="table-row divider-bottom">
        <div
          v-for="column in columns"
          :key="column.name"
          class="table-cell px4 py2">
          {{ column.title }}
        </div>
      </div>
      <div
        v-for="(row, index) in rows"
        :key="index"
        class="table-row divider-bottom">
        <div
          v-for="column in columns"
          :key="column.name"
          class="table-cell px4 py2">
          {{ row[column.name] }}
        </div>
      </div>
    </div>
    <Pager
      v-model="currentPage"
      :records="value.length"
      :per-page="perPage" />
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
      perPage: {
        type: Number,
        required: false,
        default: 5,
      },
    },
    data() {
      return {
        currentPage: 1,
      };
    },
    computed: {
      rows() {
        const offset = (this.currentPage - 1) * this.perPage;
        return this.value.slice(offset, this.perPage + offset);
      },
    },
  };
</script>
