<template>
  <span class="flex gutters flex-wrap">
    <template v-for="item in options">
      <Checkbox
        :value="isChecked(item)"
        :label="item[labelField]"
        :key="item.id"
        class="col-3"
        @input="toggle(item)" />
    </template>
  </span>
</template>

<script>
  export default {
    name: "CheckGroup",
    props: {
      value: {
        type: Array,
        required: true,
      },
      options: {
        type: Array,
        required: true,
      },
      idField: {
        type: String,
        default: "id",
      },
      labelField: {
        type: String,
        default: "name",
      },
    },
    methods: {
      isChecked(option) {
        return !!this.value.includes(option[this.idField]);
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
