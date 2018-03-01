<template>
  <span :is="container">
    <Btn
      v-for="option in options"
      :key="option[idField]"
      :class="has(option) ? 'btn-primary' : 'bg-white black'"
      :label="option[labelField]"
      class="rounded50 sm-mr1 shadow1"
      @click="toggle(option)" />
  </span>
</template>

<script>
  export default {
    name: "TagGroup",
    props: {
      value: {
        type: Array,
        default: () => [],
      },
      options: {
        type: Array,
        default: () => [],
      },
      idField: {
        type: String,
        default: "id",
      },
      labelField: {
        type: String,
        default: "name",
      },
      container: {
        type: String,
        default: "span",
      },
    },
    methods: {
      has(option) {
        return this.value.includes(option[this.idField]);
      },
      toggle(option) {
        const id = option[this.idField];
        if (this.value.includes(id)) {
          this.$emit("input", this.value.filter(item => item !== id));
        } else {
          this.$emit("input", this.value.concat(id));
        }
      },
    },
  };
</script>
