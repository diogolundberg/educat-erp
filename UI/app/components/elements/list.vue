<template>
  <div class="m2 flex flex-column items-center">
    <InputBox
      v-model="filter"
      label="Filtro" />
    <div class="block sm-table bg-white shadow2 m2 col-12 fit">
      <div class="hidden sm-table-row divider-bottom">
        <div
          v-for="column in columns"
          v-if="column.show !== false"
          :key="column.name"
          :class="{ bold: sortColumn === column.name }"
          class="block sm-table-cell px2 py2 no-select pointer truncate"
          @click="sortBy(column)">
          {{ column.title }}
          <span v-if="sortColumn === column.name && reverse">↑</span>
          <span v-if="sortColumn === column.name && !reverse">↓</span>
        </div>
      </div>
      <div
        v-for="(row, index) in rows"
        :key="index"
        class="block sm-table-row divider-bottom h-bg-silver pointer"
        @click="$emit('click', row)">
        {{ $option }}
        <div
          v-for="column in columns"
          v-if="column.show !== false"
          :key="`${index}_${column.name}`"
          class="block sm-table-cell px2 sm-px2 py1 sm-py2 truncate">
          <strong class="sm-hide md-hide lg-hide">
            {{ column.title }}:
          </strong>
          <template v-if="$scopedSlots[`column-${column.name}`]">
            <slot
              :name="`column-${column.name}`"
              :row="row" />
          </template>
          <template v-else-if="column.format == 'date'">
            {{ row[column.name] | date }}
          </template>
          <template v-else>
            {{ row[column.name] }}
          </template>
        </div>
      </div>
      <div
        v-if="!rows.length"
        class="block px2 sm-px2 py1 sm-py2">
        {{ noneMessage }}
      </div>
    </div>
    <Pager
      v-model="currentPage"
      :records="filtered.length"
      :per-page="perPage" />
  </div>
</template>

<script>
  import { sortBy } from "lodash";

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
      noneMessage: {
        type: String,
        default: "Nenhum item encontrado",
      },
    },
    data() {
      return {
        filter: "",
        currentPage: 1,
        sortColumn: null,
        reverse: false,
      };
    },
    computed: {
      rows() {
        const sorted = sortBy(this.filtered, [this.sortColumn]);
        const reversed = this.reverse ? sorted.reverse() : sorted;
        const offset = (this.currentPage - 1) * this.perPage;
        return reversed.slice(offset, this.perPage + offset);
      },
      filtered() {
        const filterRow = row => Object.values(row).some(a =>
        a && a.toString().includes(this.filter));
        return this.value.filter(filterRow);
      },
    },
    watch: {
      filter() {
        this.currentPage = 1;
      },
    },
    methods: {
      sortBy({ name }) {
        this.reverse = this.sortColumn === name ? !this.reverse : false;
        this.sortColumn = name;
      },
    },
  };
</script>
